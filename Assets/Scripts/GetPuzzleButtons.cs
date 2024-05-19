using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPuzzleButtons : MonoBehaviour
{
    public List<Button> puzzleButtons = new List<Button>();

    [SerializeField]
    private Sprite puzzleButtonImage;
    void Start()
    {
        getButtons();
    }
    void getButtons()
    {
        GameObject[] btns = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < 8; i++)
        {
            puzzleButtons.Add(btns[i].GetComponent<Button>());
            puzzleButtons[i].image.sprite = puzzleButtonImage;
        }
    }


}
