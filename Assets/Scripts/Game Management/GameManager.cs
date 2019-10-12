using Enemies;
using UnityEngine;

namespace Game_Management
{
    /// <summary>
    /// Class for handling global context based game behaviour
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        

        [SerializeField]private Spawner _spawner;
        [SerializeField]private EnemyGrid _grid;
        
        
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }

            _spawner = FindObjectOfType<Spawner>();
            _grid = FindObjectOfType<EnemyGrid>();
        }
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
        public static GameManager Instance => instance;
        public EnemyGrid Grid => _grid;
    }
}


