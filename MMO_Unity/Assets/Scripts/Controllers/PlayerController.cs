using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
	private PlayerStat _stat;
	private bool _stopSkill;

	#region override
	protected override void Init()
	{
		WorldObjectType = Define.WorldObject.Player;
		_stat = gameObject.GetComponent<PlayerStat>();
	    
		Managers.Input.MouseAction -= OnMouseEvent;
		Managers.Input.MouseAction += OnMouseEvent;

		if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
			Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
	}

	protected override void UpdateMoving()
	{
		// 몬스터가 내 사정거리보다 가까우면 공격
		if (_lockTarget != null)
		{
			_destPos = _lockTarget.transform.position;
			float distance = (_destPos - transform.position).magnitude;
			if (distance <= 1)
			{
				State = Define.State.Skill;
				return;
			}
		}

		// 이동
		Vector3 dir = _destPos - transform.position;
		dir.y = 0;
		
		if (dir.magnitude < 0.1f)
		{
			State = Define.State.Idle;
		}
		else
		{
			NavMeshAgent nma = gameObject.GetComponent<NavMeshAgent>();

			float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);

			nma.Move(dir.normalized * moveDist);

			Vector3 position = transform.position + (Vector3.up * 0.5f);
			Debug.DrawRay(position, dir.normalized, Color.grey);
			if (Physics.Raycast(position, dir, 1.0f, LayerMask.GetMask("Block")))
			{
				if (Input.GetMouseButton(0) == false)
					State = Define.State.Idle;
				return;
			}

			transform.position += dir.normalized * moveDist;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
		}
	}
	
	protected override void UpdateSkill()
	{
		if (_lockTarget != null)
		{
			Vector3 dir = _lockTarget.transform.position - transform.position;
			Quaternion quat = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
		}
	}
	#endregion

	void OnHitEvent()
	{
		if (_lockTarget != null)
		{
			Stat targetStat = _lockTarget.GetComponent<Stat>();
			targetStat.OnAttacked(_stat);
		}

		if (_stopSkill)
		{
			State = Define.State.Idle;
		}
		else
		{
			State = Define.State.Skill;
		}
	}
	
	void OnMouseEvent(Define.MouseEvent evt)
	{
		switch (State)
		{
			case Define.State.Idle:
			case Define.State.Moving:
				OnMouseEvent_IdleRun(evt);
				break;
			case Define.State.Skill:
			{
				if (evt == Define.MouseEvent.PointerUp)
					_stopSkill = true;
				break;
			}
		}
	}

	void OnMouseEvent_IdleRun(Define.MouseEvent evt)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		bool rayCasthit = Physics.Raycast(ray, out hit, 100.0f, CursorController.MOUSE_MASK);
		
		switch (evt)
		{
			case Define.MouseEvent.PointerDown:
			{
				if (rayCasthit)
				{
					_destPos = hit.point;
					State = Define.State.Moving;
					_stopSkill = false;

					if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
						_lockTarget = hit.collider.gameObject;
					else
						_lockTarget = null;
				}
				break;
			}
			case Define.MouseEvent.Press:
			{
				if (_lockTarget == null && rayCasthit)
					_destPos = hit.point;
				break;
			}
			case Define.MouseEvent.PointerUp:
				_stopSkill = true;
				break;
		}
	}
}