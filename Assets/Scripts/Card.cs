using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasClicked;

    [SerializeField]
    int row, col;

    [SerializeField]
    Sprite gold, fish, shark, unrevealed;

    SpriteRenderer renderer;

    Animator animator;

    public Choice mychoice;

    private void Start()
    {
        hasClicked = false;
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        mychoice = GameManager.instance.myboard.GetChoice(row, col);
        renderer.sprite = unrevealed;
    }

    public void PlayTurn()
    {
        animator.Play("Reveal");
        hasClicked = true;
    }

    public void ChangeImage()
    {
        Sprite current = mychoice == Choice.FISH ? fish
                        : mychoice == Choice.GOLD ? gold
                        : shark;
        renderer.sprite = current;
    }
}
