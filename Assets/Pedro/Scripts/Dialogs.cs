using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogs : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public List<Scene_Dialogs> sentences;
    public DialogsList DialogList = new DialogsList();
    public float typeSpeed = 0.2f;
    public string scene = "test";

    public Image box;
    public Image textDot;

    private int index = 0;
    bool readyForNext = false;
    bool runAllText = false;

    private void Awake()
    {
       sentences = ReadFile(scene);
       box.gameObject.SetActive(false);
        textDot.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartDiag();
    }
    
    private void Update()
    {
        //Continue to next sentence
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyForNext)
        {
            NextSentence();
            readyForNext = false;
            textDot.gameObject.SetActive(false);
        }

        //Skip sentence
        if (Input.GetKeyDown(KeyCode.Space) && !readyForNext)
        {
            runAllText = true;
        }
    }

    IEnumerator Type(float speed)
    {
        //Iterates trough each character in the sentence
        foreach(char letter in sentences[index].dialog_text)
        {
            textDisplay.text += letter;
            //Player skips text
            if (runAllText)
            {
                speed = 0.01f;
                //break;
                //textDisplay.text = sentences[index].dialog_text;
            }
            yield return new WaitForSeconds(speed);
        }
        runAllText = false;
        readyForNext = true;
        textDot.gameObject.SetActive(true);
        //StopCoroutine(Type());
    }

    //Plays next sentence if 
    public void NextSentence()
    {
        textDisplay.text = "";
        if (!sentences[index].last)
        {
            index += (index < sentences.Count - 1) ? 1 : 0;
            StartCoroutine(Type(typeSpeed));
        }
        else
        {
            box.gameObject.SetActive(false);
            StopCoroutine(Type(typeSpeed));
            index += 1;
        }
    }

    //Start Convo
    public void StartDiag()
    {
        box.gameObject.SetActive(true);
        StartCoroutine(Type(typeSpeed));
    }

    //Getting JSON info
    public List<Scene_Dialogs> ReadFile(string sceneName)
    {
        TextAsset asset = Resources.Load(sceneName) as TextAsset;
        if (asset != null)
        {
            DialogList = JsonUtility.FromJson<DialogsList>(asset.text);
        }
        return DialogList.Scene_Dialogs;
    }
}
