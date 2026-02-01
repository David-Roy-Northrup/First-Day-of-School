using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public GameObject triesOject; 
    public GameObject settingsButton;
    public GameObject cardManager;
    public Animator animator;
    public Animator personagemAnimator;
    public Text nameText;
    public Text dialogueText;
    public Dialogue dialogue;
    private Queue<string> sentences;
    public GameObject ronel;
    public GameObject dialogueBox;
    public GameObject handSprite;
    
    void Start()
    {
        sentences = new Queue<string>();
        
        StartDialogue(dialogue);
    }

    private void Update(){
        
        if (Input.anyKeyDown){
            DisplayNextSentence();
        }
    }

    IEnumerator DestroyAssets(){
        
        yield return new WaitForSeconds(2);
        Destroy(ronel);
        Destroy(dialogueBox);
        Destroy(gameObject);
    }

    public void StartDialogue(Dialogue dialogue){
       
        nameText.text = dialogue.name;
        sentences.Clear();

         if (PlayerPrefs.GetString("Language", "English") == "English")
        {
            foreach (string sentence in dialogue.englishSentences)
            {
                sentences.Enqueue(sentence);
            }
        }
        else
        {
            foreach (string sentence in dialogue.portuguesSentences)
            {
                sentences.Enqueue(sentence);
            }
        }

        DisplayNextSentence();
    }
    
    public void DisplayNextSentence(){
        
        if(sentences.Count==0){
            
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;         
    }

    public void EndDialogue(){
       
        animator.SetBool("isEnd", true);
        personagemAnimator.SetBool("isEnd", true);
        cardManager.SetActive(true);
        settingsButton.SetActive(true);
        triesOject.SetActive(true);
        handSprite.SetActive(true);
        StartCoroutine("DestroyAssets");
    }
}