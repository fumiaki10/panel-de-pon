using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class blocks:MonoBehaviour
{
    public enum PanelType
  {
    Circle,
    Diamond,
    Heart,
    Star,
    Triangle
  }
  public GameObject[] PanelPrefabs;

  public PanelType myType;

  int blockX = 0;
  int blockY = 0;

  

  void Start()
  {
  //  Instantiate(PanelPrefabs[0],new Vector3(0,0),quaternion.identity);
  //  Instantiate(PanelPrefabs[1],new Vector3(2,0),quaternion.identity);
  //  Instantiate(PanelPrefabs[2],new Vector3(4,0),quaternion.identity);
  //  Instantiate(PanelPrefabs[3],new Vector3(6,0),quaternion.identity);
  //  Instantiate(PanelPrefabs[4],new Vector3(8,0),quaternion.identity);
    for(int x = 0;x <= 5; x++)
    {
   for(int y = 0;y <= 7; y++)
      {
        int randomBlocks = UnityEngine.Random.Range(0,PanelPrefabs.Length);
      Instantiate(PanelPrefabs[randomBlocks],new Vector3(x * 2,y * 2),quaternion.identity);  
      }
    }
  }

  public void ObjChange()
  {
    
  }
}
