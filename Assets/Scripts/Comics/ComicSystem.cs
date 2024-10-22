using TMPro;
using UnityEngine;

public class ComicSystem : MonoBehaviour
{
    public SceneController sceneController;
    public GameObject[] imageObjects; // Массив объектов
    [TextArea] public string[] imageTexts; // Массив текстов
    public TextMeshProUGUI displayText; // Текстовый объект для отображения текста

    private int currentIndex = 0; // Текущий активный индекс
    private int switchCount = 0; // Количество переключений
    private int lastActiveIndex = 0; // Последний активный индекс перед переключением сцен

    void Start()
    {
        // Проверяем, есть ли активные объекты в массиве при старте
        for (int i = 0; i < imageObjects.Length; i++)
        {
            if (imageObjects[i].activeInHierarchy)
            {
                Debug.Log($"Current images: {currentIndex}. Current integer: {i}");
                currentIndex = i;
                Debug.Log("Активный объект: " + imageObjects[i].name);
                if (i < imageTexts.Length) // Добавляем проверку, чтобы избежать ошибки IndexOutOfRangeException
                {
                    displayText.text = imageTexts[i]; // Устанавливаем текст
                }
                break;
            }
        }
    }


    public void OnButtonClick(string nameScene)
    {
        // Если в массиве только один объект, просто переключаем сцену
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

        // Выключаем текущий активный объект
        imageObjects[currentIndex].SetActive(false);

        // Включаем следующий объект
        currentIndex = (currentIndex + 1) % imageObjects.Length;
        imageObjects[currentIndex].SetActive(true);

        Debug.Log("Новый активный объект: " + imageObjects[currentIndex].name);
        displayText.text = imageTexts[currentIndex];

        // Увеличиваем количество переключений
        switchCount++;

        // Если достигнуто максимальное количество переключений, переключаемся на другую сцену
        if (switchCount >= imageObjects.Length)
        {
            Debug.Log("Конец");
            imageObjects[lastActiveIndex].SetActive(true); // Возвращаем последний активный объект
            displayText.text = imageTexts[lastActiveIndex]; // Возвращаем последний активный текст
            currentIndex = lastActiveIndex; // Возвращаем последний активный индекс
            switchCount = 0;

            sceneController.LoadLevel(nameScene);
        }
        else
        {
            lastActiveIndex = currentIndex; // Сохраняем последний активный индекс
        }
    }
}


