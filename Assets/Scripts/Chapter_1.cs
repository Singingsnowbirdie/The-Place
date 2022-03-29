using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum Examined
{
    FirstFork, //первая развилка
    SecondFork, //вторая развилка
    PotionsBookRoom, //комната с книгoй по зельям
    HerbsBookRoom, //комната с книгой по травам
    Greenhouse, //оранжерея посещена
    MoonTea, //про лунный чай прочитано
    PotionHerbs, //как выглядят длиннолистник и дремотник
    SnakeHerbs, //записка про змеиные травы прочитана
    Kitchen, //посещена кухня
}

enum Items
{
    Key, //ключ из книги по зельям
    OpenDoor, //дверь на первой развилке открыта
    MoonTea, //лунный чай
    SnakeHerbs, //змеиные травы
    Slumber, //дремотник
    Longleaf //длиннолистник
}


public class Chapter_1 : MonoBehaviour
{
    //фоновая картинка
    [SerializeField] Image background;

    //картинка
    [SerializeField] Image image;

    //текст
    [SerializeField] TextMeshProUGUI text;

    //кнопка 1
    [SerializeField] Button button_1;
    //кнопка 2
    [SerializeField] Button button_2;
    //кнопка 3
    [SerializeField] Button button_3;
    //кнопка 4
    [SerializeField] Button button_4;


    //текст кнопки 1
    [SerializeField] TextMeshProUGUI buttonText_1;
    //текст кнопки 2
    [SerializeField] TextMeshProUGUI buttonText_2;
    //текст кнопки 3
    [SerializeField] TextMeshProUGUI buttonText_3;
    //текст кнопки 4
    [SerializeField] TextMeshProUGUI buttonText_4;

    //фоновые спрайты
    [SerializeField] List<Sprite> backgrounds;

    //спрайты
    [SerializeField] List<Sprite> sprites;

    //посещенные комнаты, осмотренные вещи, полученная информация
    List<Examined> examined;

    //предметы, собранные игроком
    List<Items> items;

    //текущая комната
    int currentRoom = 0;

    //стадия чтения книги
    int bookReadingStage = 0;

    private void Awake()
    {
        examined = new List<Examined>();
        items = new List<Items>();
    }

    private void Start()
    {
        ZeroRoomEnter();
    }

    public void Button1Press()
    {
        switch (currentRoom)
        {
            case 0: //нулевая комната
                ZeroRoomDialog();
                break;
            case 1: //первая развилка
                LeftDoorDialog();
                break;
            case 2: //комната с книгой по зельям
                PotionsBookDialog();
                break;
            case 3: //вторая развилка
                HerbsRoomEnter();
                break;
            case 4: //комната с книгой по травологии
                HerbsBookDialog();
                break;
            case 5: //оранжерея
                SecondForkEnter();
                break;
            case 6: //ведьмина кухня
                GreenhouseEnter();
                break;
        }
    }

    public void Button2Press()
    {
        switch (currentRoom)
        {
            case 0: //нулевая комната
                FirstForkEnter();
                break;
            case 1: //первая комната с двумя дверями
                PotionsRoomEnter();
                break;
            case 2: //комната с книгой по зельям
                FirstForkEnter();
                break;
            case 3: //вторая комната с двумя дверями
                GreenhouseEnter();
                break;
            case 4: //комната с книгой по травам
                SecondForkEnter();
                break;
            case 5: //оранжерея
                KitchenEnter();
                break;
            case 6: //ведьмина кухня
                break;
        }

    }

    public void Button3Press()
    {
        switch (currentRoom)
        {
            case 5: //оранжерея
                HerbsSearching();
                break;
        }
    }

    public void Button4Press()
    {
        switch (currentRoom)
        {

        }
    }

    //скрывает картинку и все кнопки
    void HideAll()
    {
        image.sprite = null;
        button_1.gameObject.SetActive(false);
        button_2.gameObject.SetActive(false);
        button_3.gameObject.SetActive(false);
        button_4.gameObject.SetActive(false);
    }

    //показывает картинку
    void ShowImage(int spriteID)
    {
        image.sprite = sprites[spriteID];
    }

    //показывает текст
    void ShowText(string txt)
    {
        text.text = txt;
    }

    //показывает первую кнопку
    void ShowButtonOne(string text)
    {
        button_1.gameObject.SetActive(true);
        buttonText_1.text = text;
    }

    //показывает вторую кнопку
    void ShowButtonTwo(string text)
    {
        button_2.gameObject.SetActive(true);
        buttonText_2.text = text;
    }

    //показывает третью кнопку
    void ShowButtonThree(string text)
    {
        button_3.gameObject.SetActive(true);
        buttonText_3.text = text;
    }

    //показывает четвертую кнопку
    void ShowButtonFour(string text)
    {
        button_4.gameObject.SetActive(true);
        buttonText_4.text = text;
    }

    //устанавливает фон
    void SetBackground(int backgroundID)
    {
        background.sprite = backgrounds[backgroundID];
    }

    //проверяет наличие предмета у игрока
    private bool HasItem(Items value)
    {
        foreach (var item in items)
        {
            if (item == value)
            {
                return true;
            }
        }
        return false;
    }

    //проверяет осведомленность игрока
    private bool IsExamined(Examined value)
    {
        foreach (var item in examined)
        {
            if (item == value)
            {
                return true;
            }
        }
        return false;
    }

    //появление
    void ZeroRoomEnter()
    {
        HideAll();

        string txt = "Ты стоишь в квадратной комнате без окон. Стены выкрашены в розовый. " +
            "Никакой мебели здесь нет. В стене напротив - закрытая дверь. " +
            "\nИ ты понятия не имеешь, как сюда попала.";
        ShowText(txt);

        ShowButtonOne("Ждать");
        ShowButtonTwo("Посмотреть, что за дверью");
    }

    //диалог нулевой комнаты
    void ZeroRoomDialog()
    {
        string txt = "Некоторое время ты ждешь. И ждешь. И ждешь... Но ничего не происходит.";
        ShowText(txt);
    }

    //появление у первой развилки
    void FirstForkEnter()
    {
        HideAll();
        string txt;
        currentRoom = 1;
        ShowImage(2);
        if (!IsExamined(Examined.PotionsBookRoom)) //еще не были в комнате с зельями
        {
            txt = $"Дверь за твоей спиной тут же бесследно истаивает, как и не было." +
                $"\nЭта комната идентична предыдущей во всем, кроме одного: " +
                $"отсюда ведут уже две двери - одна по левую, другая по правую руку.";
            ShowButtonTwo("Проверить правую дверь");
        }
        else 
        {
            txt = "За время твоего отсутствия здесь ничего не изменилось. Здесь все так же - две двери.";
            ShowButtonTwo("Вернуться в комнату с книгой");
        }

        ShowText(txt);

        if (HasItem(Items.OpenDoor)) //дверь открыта
            ShowButtonOne("Войти в левую дверь");
        else
            ShowButtonOne("Проверить левую дверь");
    }

    //попытки открыть левую дверь
    void LeftDoorDialog()
    {
        if (!HasItem(Items.Key)) //нет ключа
        {
            text.text = $"Левая дверь заперта.";
        }
        else if (HasItem(Items.Key) && !HasItem(Items.OpenDoor)) //есть ключ, но дверь еще не отперта
        {
            text.text = $"Ключ, обнаруженный в книге, идеально подходит к замочной скважине. " +
                $"Дверь открыта, можно идти дальше. Но прежде убедись, что правильно запомнила рецепт зелья. " +
                $"Вдруг это важно?";
            items.Add(Items.OpenDoor);
            buttonText_1.text = "Войти в левую дверь";
            buttonText_2.text = "Вернуться в комнату с книгой";
        }
        else if (HasItem(Items.OpenDoor)) //дверь отперта ключом
            SecondForkEnter();
    }

    //появление в комнате зелий
    void PotionsRoomEnter()
    {
        currentRoom = 2;
        ShowImage(0);
        if (!IsExamined(Examined.PotionsBookRoom)) //первое появление в комнате
        {
            bookReadingStage = 0;
            examined.Add(Examined.PotionsBookRoom);
            text.text = "Эта комната выглядит совершенно так же, как предыдущие. Нет окон. Розовые стены. " +
                "Единственная дверь - та, через которую ты вошла. Но в центре комнаты на напольном пюпитре покоится книга. " +
                "Большая и старинная.";
            buttonText_1.text = "Осмотреть книгу";
        }
        else //последующие появления
        {
            text.text = "В центре комнаты на напольном пюпитре покоится книга с изображениями склянок, грибов и растений. " +
                    "Надпись на обложке гласит: \"Руководство по зельям для начинающих\"";
            buttonText_1.text = "Перечитать книгу";
        }

        buttonText_2.text = "Вернуться к развилке";
    }

    //чтение книги по зельям
    void PotionsBookDialog()
    {
        if (bookReadingStage == 0)
        {
            bookReadingStage = 1;
            text.text = "Книга украшена изображениями склянок, грибов и растений. " +
                "Надпись на обложке гласит: \"Руководство по зельям для начинающих\"";
            buttonText_1.text = "Открыть книгу";
        }
        else if (bookReadingStage == 1)
        {
            bookReadingStage = 2;
            ShowImage(3);
            text.text = "\n\"Одну часть длиннолистника и две части дремотника измельчите и поместите в холодную воду. " +
                "Вскипятите, помешивая по часовой стрелке. Добавьте перо неразлучника. Перелейте в чистую склянку. Приворотное зелье готово!\"";
            buttonText_1.text = "Продолжить читать";
        }
        else if (bookReadingStage == 2)
        {
            bookReadingStage = 3;
            ShowImage(0);
            if (!HasItem(Items.Key)) //если ключ еще не взят из книги
            {
                text.text = "Как ни странно, все остальные страницы книги пусты. " +
                    "Но между четвертой и пятой вложен небольшой серебристый ключик.";
                buttonText_1.text = "Забрать ключ и закрыть книгу";
                items.Add(Items.Key);
            }
            else //если ключ уже взят из книги
            {
                ShowImage(0);
                text.text = "Все остальные страницы книги пусты, а ключ ты уже забрала.";
                buttonText_1.text = "Закрыть книгу";
            }
        }
        else
        {
            bookReadingStage = 1;
            text.text = "Книга украшена изображениями склянок, грибов и растений. " +
                "Надпись на обложке гласит: \"Руководство по зельям для начинающих\"";
            buttonText_1.text = "Перечитать книгу";
        }
    }

    //появление у второй развилки
    void SecondForkEnter()
    {
        currentRoom = 3;
        ShowImage(2);
        if (!IsExamined(Examined.SecondFork))
        {
            examined.Add(Examined.SecondFork);
            text.text = "Едва ты переступаешь порог комнаты, как дверь за твоей спиной истаивает. Назад пути больше нет." +
                "\nЭта комната - близнец предыдущей. В ней тоже две двери.";
        }
        else
            text.text = "За время твоего отсутствия здесь ничего не изменилось. Здесь все так же две двери.";

        if (!IsExamined(Examined.HerbsBookRoom)) //комната с книгой про травы не посещена
            buttonText_1.text = "Проверить левую дверь";
        else
            buttonText_1.text = "Отправиться в комнату с книгой";


        if (!IsExamined(Examined.Greenhouse)) //теплица не посещена
            buttonText_2.text = "Проверить правую дверь";
        else
            buttonText_2.text = "Отправиться в оранжерею";

        button_3.gameObject.SetActive(false);
    }

    //появление в комнате травологии
    void HerbsRoomEnter()
    {
        currentRoom = 4;
        ShowImage(0);
        examined.Add(Examined.HerbsBookRoom);
        bookReadingStage = 0;
        text.text = "Еще одна комната с книгой. На обложке написано: \"Справочник по грибам и травам для начинающих.\" Будем читать?";
        buttonText_1.text = "Полистать книгу";
        buttonText_2.text = "Выйти из комнаты";
    }

    //читаем книгу о травах
    void HerbsBookDialog()
    {
        if (bookReadingStage == 0)
        {
            ShowImage(6);
            bookReadingStage++;
            examined.Add(Examined.PotionHerbs);
            text.text = "Листая книгу, ты узнала как выглядят длиннолистник и дремотник. Второй, оказывается, гриб. " +
                "И очень опасный. При прикосновении он выделяет споры, способные усыпить человека насмерть. " +
                "К счастью, от него есть противоядие - лунный чай.";
            buttonText_1.text = "Полистать книгу еще";
        }
        else if (bookReadingStage == 1)
        {
            bookReadingStage++;
            ShowImage(4);
            examined.Add(Examined.MoonTea);
            text.text = "Лунный чай изготавливается из высушенных ферментированных листьев одноименного растения. " +
                "Сам напиток готовится точно так же, как и его неволшебный аналог.";
        }
        else if (bookReadingStage == 2)
        {
            ShowImage(5);
            bookReadingStage++;
            text.text = "Внезапно, из книги выпадает какой-то листок. " +
                "Это тоже страница, но судя по размеру и шрифту, она было вырвана из какой-то другой книги. " +
                "На картинке нарисована змея.";
            buttonText_1.text = "Изучить текст на листке";
        }
        else if (bookReadingStage == 3)
        {
            bookReadingStage++;
            text.text = "\"Охраняет от змей можжевельник, базилик, рута, вероника и папоротник. " +
                "Еще пуще змеи не выносят их запах. " +
                "Если взять некоторые из этих растений, и истолочь их в массу, то можно...\"" +
                "\nНа этом запись обрывается.";
            buttonText_1.text = "Поискать в книге упомянутые растения";
        }
        else if (bookReadingStage == 4)
        {
            ShowImage(1);
            bookReadingStage = 0;
            examined.Add(Examined.SnakeHerbs);
            text.text = "Нужные растения отыскиваются легко. " +
                "Теперь ты знаешь как они выглядят, где растут, и как их правильно собирать. " +
                "Всё это - растения неволшебные и не ядовитые, так что никаких особых предосторожностей в обращении с ними не требуется.";
            buttonText_1.text = "Перечитать книгу";
        }
    }

    //появление в оранжерее
    void GreenhouseEnter()
    {
        currentRoom = 5;
        ShowImage(1);
        if (!IsExamined(Examined.Greenhouse))
        {
            examined.Add(Examined.Greenhouse);
            text.text = "За дверью обнаруживается просторная оранжерея. " +
                "Стеклянный потолок и дополнительные лампы, свисающие с потолка, дают растениям и грибам достаточно необходимого света. " +
                "В дальнем конце оранжереи имеется еще одна дверь.";
        }
        else
        {
            text.text = "Просторная оранжерея. " +
                "Стеклянный потолок и дополнительные лампы, свисающие с потолка, дают растениям и грибам достаточно необходимого света. ";
        }
        buttonText_1.text = "Вернуться к развилке";


        if (!IsExamined(Examined.Kitchen))
            buttonText_2.text = "Проверить дверь";
        else
            buttonText_2.text = "На кухню";

        button_3.gameObject.SetActive(true);
        buttonText_3.text = "Поискать нужные растения";
    }

    //поиски растений в оранжерее
    void HerbsSearching()
    {
        if (!IsExamined(Examined.PotionHerbs)) //книга по травам еще не прочитана
            text.text = "К сожалению, ты понятия не имеешь, как выглядят длиннолистник и дремотник.";
        else if (IsExamined(Examined.PotionHerbs) && !HasItem(Items.Longleaf)) //книга прочитана, длиннолистник не собран
        {
            text.text = "Следуя рекомендациям книги, ты легко находишь и собираешь длиннолистник.";
            items.Add(Items.Longleaf);
        }
        else if (HasItem(Items.MoonTea)) //есть лунный чай
        {
            ShowImage(6);
            text.text = "Перед тем, как собирать дремотник, ты выпиваешь лунный чай, и сонные споры не причиняют тебе никакого вреда.";
            items.Add(Items.Slumber);
        }
        else if (IsExamined(Examined.SnakeHerbs) && !HasItem(Items.SnakeHerbs)) //есть знания о змеиных травах, но нет трав
        {
            items.Add(Items.SnakeHerbs);
            text.text = "Отыскались рута, можжевельник и базилик. " +
                "Причем, судя по количеству сортов последнего, где-то тут точно должна быть кухня. ";
        }
        else if (!HasItem(Items.Slumber) && !HasItem(Items.MoonTea)) //нет дремотника, нет чая
        {
            ShowImage(6);
            text.text = "Ты легко находишь дремотник, но прикасаться к нему пока не стоит; ты еще не выпила лунный чай.";
        }
        else
        {
            ShowImage(1);
            text.text = "В оранжерее еще много грибов и растений, но никакие из них тебе пока не нужны.";
        }
    }

    //появление на кухне
    void KitchenEnter()
    {
        currentRoom = 6;
        ShowImage(7);
        examined.Add(Examined.Kitchen);
        text.text = "А вот и ведьмина кухня. " +
            "Здесь повсюду баночки, скляночки, котлы с чугунками, пахнет специями и выпечкой, а в дальнем углу примостилась печка.";
        buttonText_1.text = "Вернуться в оранжерею";
        buttonText_2.text = "Поискать что-нибудь полезное";
        button_3.gameObject.SetActive(false);
    }


}
