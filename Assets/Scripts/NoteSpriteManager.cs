using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSpriteManager : MonoBehaviour
{

    [SerializeField] private List<Sprite> noteList;

    [SerializeField] private Image displayedNote;

    private int nbDiffNotes;

    private int currentNoteIndex;

    private float score;

    private float noteScoreStep;

    // Start is called before the first frame update
    void Start()
    {
        nbDiffNotes = noteList.Count;

        noteScoreStep = 100.0f / nbDiffNotes;

        currentNoteIndex = 0;

        displayedNote.sprite = noteList[currentNoteIndex];
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/


    public void UpdateScore(float newScore)
    {
        SetScore(newScore);

        ChangeNote();
    }


    public void SetScore(float newScore)
    {
        score = newScore;
    }

    private void ChangeNote()
    {
        if(score > (1 + currentNoteIndex) * noteScoreStep)
        {
            UpgradeNote();
        }
        else
        {
            if(score < (currentNoteIndex) * noteScoreStep)
            {
                DowngradeNote();
            }
        }
    }


    private void UpgradeNote()
    {
        if(currentNoteIndex >= nbDiffNotes - 1)
        {
            return;
        }

        currentNoteIndex++;

        displayedNote.sprite = noteList[currentNoteIndex];
    }

    private void DowngradeNote()
    {
        if (currentNoteIndex <= 0)
        {
            return;
        }

        currentNoteIndex--;

        displayedNote.sprite = noteList[currentNoteIndex];
    }
}
