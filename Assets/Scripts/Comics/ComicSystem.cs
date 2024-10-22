using TMPro;
using UnityEngine;

public class ComicSystem : MonoBehaviour
{
    public SceneController sceneController;
    public GameObject[] imageObjects; // ������ ��������
    [TextArea] public string[] imageTexts; // ������ �������
    public TextMeshProUGUI displayText; // ��������� ������ ��� ����������� ������

    private int currentIndex = 0; // ������� �������� ������
    private int switchCount = 0; // ���������� ������������
    private int lastActiveIndex = 0; // ��������� �������� ������ ����� ������������� ����

    void Start()
    {
        // ���������, ���� �� �������� ������� � ������� ��� ������
        for (int i = 0; i < imageObjects.Length; i++)
        {
            if (imageObjects[i].activeInHierarchy)
            {
                Debug.Log($"Current images: {currentIndex}. Current integer: {i}");
                currentIndex = i;
                Debug.Log("�������� ������: " + imageObjects[i].name);
                if (i < imageTexts.Length) // ��������� ��������, ����� �������� ������ IndexOutOfRangeException
                {
                    displayText.text = imageTexts[i]; // ������������� �����
                }
                break;
            }
        }
    }


    public void OnButtonClick(string nameScene)
    {
        // ���� � ������� ������ ���� ������, ������ ����������� �����
        if (imageObjects.Length == 1)
        {
            sceneController.LoadLevel(nameScene);
            if (currentIndex > 0 && switchCount > 1)
            {
                currentIndex = 0;
                switchCount = 1;
            }
            return;
        }

        // ��������� ������� �������� ������
        imageObjects[currentIndex].SetActive(false);

        // �������� ��������� ������
        currentIndex = (currentIndex + 1) % imageObjects.Length;
        imageObjects[currentIndex].SetActive(true);

        Debug.Log("����� �������� ������: " + imageObjects[currentIndex].name);
        displayText.text = imageTexts[currentIndex];

        // ����������� ���������� ������������
        switchCount++;

        // ���� ���������� ������������ ���������� ������������, ������������� �� ������ �����
        if (switchCount >= imageObjects.Length)
        {
            Debug.Log("�����");
            imageObjects[lastActiveIndex].SetActive(true); // ���������� ��������� �������� ������
            displayText.text = imageTexts[lastActiveIndex]; // ���������� ��������� �������� �����
            currentIndex = lastActiveIndex; // ���������� ��������� �������� ������
            switchCount = 0;

            sceneController.LoadLevel(nameScene);
        }
        else
        {
            lastActiveIndex = currentIndex; // ��������� ��������� �������� ������
        }
    }
}


