using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    private int _monsterCount;
    private int _reserveCount;

    [SerializeField]
    private int _keepMonsterCount;
    
    [SerializeField]
    Vector3 _spawnPos;
    
    [SerializeField]
    float _spawnRadius = 15.0f;

    [SerializeField]
    private float _spawnTime = 5.0f;

    public void AddMonsterCount(int value)
    {
        _monsterCount += value;
    }

    public void SetKeepMonsterCount(int value)
    {
        _keepMonsterCount = value;
    }
    
    void Start()
    {
        Managers.Game.OnSpanEvent -= AddMonsterCount;
        Managers.Game.OnSpanEvent += AddMonsterCount;
    }

    void Update()
    {
        while (_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, _spawnTime));
        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "DogKnight");
        NavMeshAgent nma = obj.GetComponent<NavMeshAgent>();
        
        Vector3 randPos;
        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
            randDir.y = 0;
            randPos = _spawnPos + randDir;
            
            NavMeshPath path = new NavMeshPath();
            if (nma.CalculatePath(randPos, path))
                break;
        }

        obj.transform.position = randPos;
        _reserveCount--;
    }
}
