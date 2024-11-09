using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, IPointerDownHandler
{
    public Text dialogueText;
    public GameObject nextText;
    public CanvasGroup dialoguegroup;
    public Queue<string> sentences;

    private string currentSentence;
    public float typingSpeed = 0.1f;
    private bool istyping;

    public static DialogueManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        sentences = new Queue<string>(); //대화를 담기위한 Queue
    }

    public void Ondialogue(string[] lines)
    {
        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        dialoguegroup.alpha = 1; //대화문 켜기
        dialoguegroup.blocksRaycasts = true; //true시 마우스이벤트 감지

        NextSentence();
    }

    public void NextSentence()
    {
        if(sentences.Count != 0)
        {
            currentSentence = sentences.Dequeue(); //가장 먼저 들어온 데이터를 반환 및 제거
            istyping = true;
            nextText.SetActive(false);
            StartCoroutine(Typing(currentSentence)); //코루틴 실행   
        }
        else
        {
            dialoguegroup.alpha = 0;
            dialoguegroup.blocksRaycasts = false;
        }
    }

    IEnumerator Typing(string line)
    {
        dialogueText.text = ""; //빈 문자열로 초기화
        foreach(char letter in line.ToCharArray()) //char형 배열로 변환
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void Update()
    {
        if (dialogueText.text.Equals(currentSentence))
        {
            nextText.SetActive(true);
            istyping = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData) //해당 스크립트가 붙은 오브젝트에 클릭, 터치가 있을 때 호출 됨
    {
        if(!istyping) NextSentence();
    }
}
