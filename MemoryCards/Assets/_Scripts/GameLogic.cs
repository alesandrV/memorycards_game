using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;

    public Sprite[] images;

    public List<Sprite> gameImages = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessImage, secondGuessImage;

    private void Start()
    {
        GetButtons();
        AddListeners();
        AddGameImages();
        Shuffle(gameImages);
        gameGuesses = gameImages.Count / 2;
    }

    // Find all buttons created with the Main script and add an image to them (background).
    private void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Button");

        for(int i = 0; i<objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    // Add front image to our buttons 2 times.
    private void AddGameImages()
    {
        int looper = btns.Count;
        int index = 0;

        for(int i = 0; i < looper; i++)
        {
            if(index == looper / 2)
            {
                index = 0;
            }

            gameImages.Add(images[index]);

            index++;
        }
    }

    // Add PickAnImage function to buttons created after the game starts.
    private void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAnImage());
        }
    }

    // "Press the button" logic with getting the images` name.
    public void PickAnImage()
    {
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessImage = gameImages[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gameImages[firstGuessIndex];
        }
        else if(!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessImage = gameImages[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gameImages[secondGuessIndex];

            countGuesses++;

            StartCoroutine(CheckIfImagesMatch());
        }
    }

    // Сheck the answer and "hide" chosen buttons or turn them back again.
    IEnumerator CheckIfImagesMatch()
    {
        yield return new WaitForSeconds(1f);

        if(firstGuessImage == secondGuessImage)
        {
            yield return new WaitForSeconds(0.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfGameFinished();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(0.5f);

        firstGuess = secondGuess = false;
    }

    private void CheckIfGameFinished()
    {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game Fifnished"); // That`s all I want to add right now.
        }
    }

    private void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
