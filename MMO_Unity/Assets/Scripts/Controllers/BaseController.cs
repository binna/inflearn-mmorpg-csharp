using UnityEngine;

public abstract class BaseController  : MonoBehaviour
{
	protected Vector3 _destPos;
	
	protected GameObject _lockTarget;
	
	[SerializeField]
	private Define.State _state = Define.State.Idle;

	public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

	public virtual Define.State State 
	{ 
		get { return _state; }
		set
		{
			_state = value;
			
			Animator animator = GetComponent<Animator>();
			switch (_state)
			{
				case Define.State.Die:
					// TODO
					break;
				case Define.State.Idle:
					animator.CrossFade("WAIT", 0.1f);
					break;
				case Define.State.Moving:
					animator.CrossFade("RUN", 0.1f);
					break;
				case Define.State.Skill:
					animator.CrossFade("ATTACK", 0.1f, -1, 0);
					break;
			}
		}
	}

	private void Start()
	{
		Init();
	}

	void Update()
	{
		switch (State)
		{
			case Define.State.Die:
				UpdateDie();
				break;
			case Define.State.Moving:
				UpdateMoving();
				break;
			case Define.State.Idle:
				UpdateIdle();
				break;
			case Define.State.Skill:
				UpdateSkill();
				break;
		}
	}

	protected abstract void Init();
	
	protected virtual void UpdateDie() { }
	protected virtual void UpdateMoving() { }
	protected virtual void UpdateIdle() { }
	protected virtual void UpdateSkill() { }
}