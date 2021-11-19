using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Text message;

    public Board myboard;

    public static GameManager instance;

    bool hasGameFinsihed;
    public void GameRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        message.text = "Play the Next Turn";
        hasGameFinsihed = false;
        myboard = new Board();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            //if hasgameFinsished
            if (hasGameFinsihed) return;

            //RayCast2D
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (!hit.collider) return;

            if(hit.collider.CompareTag("Card"))
            {
                Card card = hit.collider.gameObject.GetComponent<Card>();

                if (card.hasClicked) return;

                card.PlayTurn();

                if(card.mychoice == Choice.GOLD)
                {
                    hasGameFinsihed = true;
                    message.text = "You Win!";
                }
                else if (card.mychoice == Choice.SHARK)
                {
                    hasGameFinsihed = true;
                    message.text = "You Lose...";
                }
            }
        }
    }
}
