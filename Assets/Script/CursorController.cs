using UnityEngine;
using UnityEngine.InputSystem;
public class CursorController:MonoBehaviour
{
  void Start()
  {
    
  }

 int cursorX = 0;
   int cursorY = 0; 

   float gridSize = 1.0f;

   int maxX = 4;
   int maxY = 10;

  void Update()
  {
   
    var keyboard = Keyboard.current;
    if(keyboard == null) return;

  if(keyboard.leftArrowKey.wasPressedThisFrame || keyboard.aKey.wasPressedThisFrame)
    {
      if(cursorX > 0) cursorX--;
      
    }
  
  if(keyboard.rightArrowKey.wasPressedThisFrame || keyboard.dKey.wasPressedThisFrame)
    {
      if(cursorX < maxX) cursorX++;
    }
  if(keyboard.upArrowKey.wasPressedThisFrame || keyboard.wKey.wasPressedThisFrame)
    {
      if(cursorY < maxY) cursorY++;
    }
    if(keyboard.downArrowKey.wasPressedThisFrame || keyboard.sKey.wasPressedThisFrame)
    {
      if(cursorY > 0) cursorY--;
    }
  
  transform.position = new Vector3(cursorX * gridSize,cursorY * gridSize, -1f);

  
    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    {
      blocks blockManager = FindAnyObjectByType<blocks>();
      // Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

      // RaycastHit2D hit2D = Physics2D.Raycast(worldPosition,Vector2.zero);
    if(blockManager != null)
      {
      blockManager.SwapBlocks(cursorX,cursorY);  
      }
      
    }
  }
}
