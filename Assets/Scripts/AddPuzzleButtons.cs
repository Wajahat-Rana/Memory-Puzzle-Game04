using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPuzzleButtons : MonoBehaviour
{
    [SerializeField] 
    private GameObject puzzleButtonPrefab;
    [SerializeField]
    private Transform puzzleFieldTransform;
   void Awake(){
    for(int i=0;i<8;i++){
    GameObject puzzleButton = Instantiate(puzzleButtonPrefab);
    puzzleButton.name = "" + i;
    puzzleButton.transform.SetParent(puzzleFieldTransform,false);
    }
   }
}
