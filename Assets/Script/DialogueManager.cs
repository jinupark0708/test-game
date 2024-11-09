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
        sentences = new Queue<string>(); //��ȭ�� ������� Queue
    }

    public void Ondialogue(string[] lines)
    {
        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        dialoguegroup.alpha = 1; //��ȭ�� �ѱ�
        dialoguegroup.blocksRaycasts = true; //true�� ���콺�̺�Ʈ ����

        NextSentence();
    }

    public void NextSentence()
    {
        if(sentences.Count != 0)
        {
            currentSentence = sentences.Dequeue(); //���� ���� ���� �����͸� ��ȯ �� ����
            istyping = true;
            nextText.SetActive(false);
            StartCoroutine(Typing(currentSentence)); //�ڷ�ƾ ����   
        }
        else
        {
            dialoguegroup.alpha = 0;
            dialoguegroup.blocksRaycasts = false;
        }
    }

    IEnumerator Typing(string line)
    {
        dialogueText.text = ""; //�� ���ڿ��� �ʱ�ȭ
        foreach(char letter in line.ToCharArray()) //char�� �迭�� ��ȯ
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

    public void OnPointerDown(PointerEventData eventData) //�ش� ��ũ��Ʈ�� ���� ������Ʈ�� Ŭ��, ��ġ�� ���� �� ȣ�� ��
    {
        if(!istyping) NextSentence();
    }
}
