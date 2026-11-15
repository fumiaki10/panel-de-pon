using System.Collections.Generic;
using Shapes2D;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
//　型を定義することで当たり判定を取得時どのパネルなのか判断させる
 public enum PanelType
  {
    Circle,
    Diamond,
    Heart,
    Star,
    Triangle
  }
public class blocks:MonoBehaviour
{
   // パネルを入れておく配列
  public GameObject[] PanelPrefabs;

[System.NonSerialized]
// ボードの作成
  public GameObject[,] board = new GameObject[6,9];

 
  
  void Start()
  {
    for(int x = 0;x <= 5; x++)
    {
   for(int y = 0;y <= 7; y++)
      {
       
        int randomBlocks = UnityEngine.Random.Range(0,PanelPrefabs.Length);
         while(x >= 2 && 
         (PanelPrefabs[randomBlocks].GetComponent<Panel>().myType == 
         board[x-1,y].GetComponent<Panel>().myType && 
         PanelPrefabs[randomBlocks].GetComponent<Panel>().myType ==
         board[x - 2, y].GetComponent<Panel>().myType) 
         || 
         y >= 2 &&
         (PanelPrefabs[randomBlocks].GetComponent<Panel>().myType == 
         board[x,y -1].GetComponent<Panel>().myType && 
         PanelPrefabs[randomBlocks].GetComponent<Panel>().myType ==
         board[x, y -2].GetComponent<Panel>().myType))
        {
          randomBlocks = UnityEngine.Random.Range(0,PanelPrefabs.Length);
        }
          GameObject newBlock = Instantiate(PanelPrefabs[randomBlocks],new Vector3(x + 0.5f ,y + 0.5f),quaternion.identity);

          board[x,y] = newBlock;

      newBlock.transform.SetParent(this.transform);
      }
    }
  }

   public void SwapBlocks(int cursorX,int cursorY)
  {
   int leftX = cursorX;
   int rightX = cursorX + 1;
   int y = cursorY;

   if(rightX >= 6) return;

   GameObject leftBlock = board[leftX,y];
   GameObject rightBlock = board[rightX,y];

   board[leftX,y] = rightBlock;
   board[rightX,y] = leftBlock;

   if(leftBlock != null)
    {
      leftBlock.transform.position = new Vector3(rightX + 0.5f, y+ 0.5f , 0f);
    }
    if(rightBlock != null)
    {
      rightBlock.transform.position = new Vector3(leftX +0.5f , y+ 0.5f , 0f);
    }

  CheckMatches();

  }
  void CheckMatches()
  {
    // 消す予定のパネルを格納するリスト
    List<Vector2Int> destroyPanel = new List<Vector2Int>();
    for(int x = 0;x <= 3; x++)
    {
      for(int y = 0;y <= 8; y++)
      {
        if(board[x,y] != null && board[x + 1,y] != null && board[x + 2,y] != null)
        {
          PanelType type1 = board[x,y].GetComponent<Panel>().myType;
          PanelType type2 = board[x + 1,y].GetComponent<Panel>().myType;
          PanelType type3 = board[x + 2,y].GetComponent<Panel>().myType;
          

          if(type1 == type2 && type2 == type3)
          {
            Debug.Log($"横で三つそろった！種類:{type1}場所:({x},{y})");
            destroyPanel.Add(new Vector2Int(x,y));
            destroyPanel.Add(new Vector2Int(x+1,y));
            destroyPanel.Add(new Vector2Int(x+2,y));
          }
        }
      }
    }
      for(int x = 0;x <= 5; x++)
    {
      for(int y = 0;y <= 6; y++)
      {
        if(board[x,y] != null && board[x ,y+ 1] != null && board[x ,y+ 2] != null)
        {
          PanelType type1 = board[x,y].GetComponent<Panel>().myType;
          PanelType type2 = board[x ,y+ 1].GetComponent<Panel>().myType;
          PanelType type3 = board[x ,y+ 2].GetComponent<Panel>().myType;

          //縦に3つ並んでいるか調べる処理
          //board[x,y + 1]のようにして縦を見れるようにする
          

          if(type1 == type2 && type2 == type3)
          {
            Debug.Log($"縦で三つそろった！種類:{type1}場所:({x},{y})");
            destroyPanel.Add(new Vector2Int(x,y));
            destroyPanel.Add(new Vector2Int(x,y+1));
            destroyPanel.Add(new Vector2Int(x,y+2));
          }
        }
      }
    }
   foreach(var item in destroyPanel)
    {
      if(board[item.x,item.y] != null)
      {
        Destroy(board[item.x,item.y]);   
        board[item.x,item.y] = null;
      }

    }
//パネルを消した後下にパネルが落ちるようにする
for(int x = 0;x <= 5; x++)
    {
      for(int y = 0;y <= 6; y++)
      {
        if(board[x,y] == null)
        {
          for(int aboveY = y + 1;aboveY <= 7; aboveY++)
          {
            if(board[x,aboveY] != null)
            {
              // 順にboardをチェックして、nullだった場合その上のブロックを代入する
              // 下から上に向かったyを0~7まで順番に見る
              // もしboard[x,y]がnullだった場合、その真上(y + 1から一番上まで)を探す
              // 上のほうで、最初に見つかった「からじゃないパネル(board[x,aboveY] != null)」を今見ているnull(空っぽの場所board[x,y]に引っ越しさせる)
              // 引っ越しさせたら、元の場所board[x,aboveY]をnullにして、画面上の位置（transform.position）も新しいyの位置に更新

              board[x,y] = board[x,aboveY];
              board[x,aboveY] = null;

              board[x,y].transform.position = new Vector3(x + 0.5f, y+ 0.5f , 0f);
              break;
            }
          }
        }
      }
    }
  }
  void Update()
  {
   
  }

 
}
