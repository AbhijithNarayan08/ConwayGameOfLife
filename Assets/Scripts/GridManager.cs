using System;
using System.Threading.Tasks;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Specify the Size of the square grid of minimum size 20")]
    [Min(20)]
    public int m_GridSize = 0;

    [SerializeField]
    public GridFactory m_GridFactory;

    [HideInInspector]
    public static event Action genCounterEvent;

    private int height = 0;

    private int width = 0;

    private CellObject[,] cellObjects;

    private const float gridSpace = 1;

    private const float delay = 0.5f;

    private bool isPlaying = false;

    private bool isOverPopulated = false;

    private bool isUnderPopulated = false;

    private bool isOptimalPopulation = false;


    private void OnEnable() 
    {
        UiController.StartEvent += startGame;
        UiController.ClearEvent += clearBoard;
    }

    private void clearBoard()
    {

        for ( int x = 0; x < height; x++ )
        {
            for ( int y = 0; y < width; y++ )
            {
                cellObjects[x,y].SetStatus(false);
            }
        }
        
        isPlaying = false;
    }

    private void startGame()
    {
        isPlaying = true;
    }

    private void Start()
    {
        height = width = m_GridSize;
        createGrid();
        
        customUpdate();
    }

    async void customUpdate()
    {
        if (Application.isPlaying)
        {
            await   Task.Delay(TimeSpan.FromSeconds( delay ));

            countNeighbours();

            if(isPlaying)
            {
                ControllCellPopulation();
                genCounterEvent?.Invoke();
            }
            customUpdate();

            Debug.Log("update called");
        }
        else
        {
            Debug.Log("Game Ended");
        }
        
    }
    private void countNeighbours()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int numNeighbours = 0;


                /// <summary>
                /// x ^ x
                /// x 0 x
                /// x x x
                /// </summary>
                if (y + 1 < height)
                {
                    if (cellObjects[x, y + 1].m_IsAlive) numNeighbours++;
                }

                /// <summary>
                /// x x x
                /// ^ 0 x
                /// x x x
                /// </summary>
                if (x + 1 < width)
                {
                    if (cellObjects[x + 1, y].m_IsAlive) numNeighbours++;
                }

                /// <summary>
                /// x x x
                /// x 0 x
                /// x ^ x
                /// </summary>

                if (y - 1 >= 0)
                {
                    if (cellObjects[x, y - 1].m_IsAlive) numNeighbours++;
                }

                /// <summary>
                /// x x x
                /// x 0 ^
                /// x x x
                /// </summary>
                if (x - 1 >= 0)
                {
                    if (cellObjects[x - 1, y].m_IsAlive) numNeighbours++;
                }

                /// <summary>
                /// x x ^
                /// x 0 x
                /// x x x
                /// </summary>

                if (x + 1 < width && y + 1 < height)
                {
                    if (cellObjects[x + 1, y + 1].m_IsAlive) numNeighbours++;
                }

                /// <summary>
                /// x x x
                /// ^ 0 x
                /// x x x
                /// </summary>
                if (x - 1 >= 0 && y + 1 < height)
                {
                    if (cellObjects[x - 1, y + 1].m_IsAlive) numNeighbours++;
                }
                /// <summary>
                /// x x x
                /// x 0 x
                /// x x ^
                /// </summary>
                if (x + 1 < width && y - 1 >= 0)
                {
                    if (cellObjects[x + 1, y - 1].m_IsAlive) numNeighbours++;
                }

                /// <summary>
                /// x x x
                /// x 0 x
                /// ^ x x
                /// </summary>
                if (x - 1 >= 0 && y - 11 >= 0)
                {
                    if (cellObjects[x - 1, y - 1].m_IsAlive) numNeighbours++;
                }

                cellObjects[x, y].m_NumNeighbours = numNeighbours;

            }
        }
    }

    public void ControllCellPopulation()
    {
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (cellObjects[x, y].m_IsAlive)
                {
                    isOverPopulated = cellObjects[x, y].m_NumNeighbours > 3;
                    isUnderPopulated = cellObjects[x, y].m_NumNeighbours < 2;
                    cellObjects[x, y].SetStatus(!(isOverPopulated || isUnderPopulated));
                }
                else
                {
                    isOptimalPopulation = cellObjects[x, y].m_NumNeighbours == 3;
                    cellObjects[x, y].SetStatus(isOptimalPopulation);
                }
            }
        }
    }

    /// <summary>fucntion for creating the gamegrid</summary>

    private void createGrid()
    {
        cellObjects = new CellObject[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                cellObjects[j, i] = m_GridFactory.GetNewInstance(new Vector3(j * gridSpace, i * gridSpace), Quaternion.identity);
                var cell = cellObjects[j, i];
                cell.gameObject.name = "Cell " + j + ", " + i;
                cell.gameObject.transform.parent = transform;
                cell.SetStatus(false);
            }
        }
    }

    void OnDisable() 
    {
        UiController.StartEvent -= startGame;
        UiController.ClearEvent -= clearBoard;
    }

}
