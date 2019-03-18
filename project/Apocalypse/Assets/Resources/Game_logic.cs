using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_logic : MonoBehaviour { 

    public Text text_game;
    public Text choice_t1;
    public Text choice_t2;
    public Text choice_t3;
    public Text choice_t4;
    public Text current_level;
    public Text screen_resolution;
    public Text meaning;
    public Text deaths;
    public Text likesImage; // с ней связаны: countLikesImage boolLikesImage bool_likes_image 

    public Button choice_b1;
    public Button choice_b2;
    public Button choice_b3;
    public Button choice_b4;

    public Scrollbar scrollbar_text;
    public Image image;

    public Slider Health;

    private Vector2 choice_v1; // После изменения значений этих векторов (choice_v№) сразу же присваиваем их первоначальное значение обратно
    private Vector2 choice_v2;
    private Vector2 choice_v3;
    private Vector2 choice_v4;

    private Vector2 choice_sv1;
    private Vector2 choice_sv2;
    private Vector2 choice_sv3;
    private Vector2 choice_sv4;

    private int tagLvl; 
    private int countMeaning;
    private int countDeaths;

    private int[] countLikesImage = new int [4];

    private bool bool_image; /* при указании: ...gameObject.SetActive(...) обязательно присваивать этим переменным соответсвующие логические значения
    Они нужны, чтобы работать с тем фактом: активны ли эти компоненты? */
    private bool bool_choice_1;
    private bool bool_choice_2;
    private bool bool_choice_3;
    private bool bool_choice_4;
    private bool bool_likes_image; 

    private bool[] boolLikesImage = new bool [4]; 


    // Use this for initialization
    void Start () {

        choice_v1 = choice_b1.transform.position;
        choice_v2 = choice_b2.transform.position;
        choice_v3 = choice_b3.transform.position;
        choice_v4 = choice_b4.transform.position;

        if (PlayerPrefs.HasKey("Save")) // Если есть сохранение, достаём сохранённые значения
        {
            tagLvl = PlayerPrefs.GetInt("Lvl");
            countMeaning = PlayerPrefs.GetInt("Meaning");
            countDeaths = PlayerPrefs.GetInt("Deaths");
            Health.value = PlayerPrefs.GetFloat("Health");
            if (PlayerPrefs.HasKey("image_count_likes_0") == true && PlayerPrefs.HasKey("ability_to_put_like_0") == true)
            {
                for (int i = 0; i < 4; i++)
                {
                    countLikesImage[i] = PlayerPrefs.GetInt("image_count_likes_" + i.ToString());
                    if (PlayerPrefs.GetInt("ability_to_put_like_" + i.ToString()) == 1)
                    {
                        boolLikesImage[i] = true;
                    }
                    else
                    {
                        boolLikesImage[i] = false;
                    }
                }
            }
            
            // достаём позицию кнопок из сохранения 
            choice_sv1.x = PlayerPrefs.GetFloat("choice_sv1_x");
            choice_sv1.y = PlayerPrefs.GetFloat("choice_sv1_y");
            choice_sv2.x = PlayerPrefs.GetFloat("choice_sv2_x");
            choice_sv2.y = PlayerPrefs.GetFloat("choice_sv2_y");
            choice_sv3.x = PlayerPrefs.GetFloat("choice_sv3_x");
            choice_sv3.y = PlayerPrefs.GetFloat("choice_sv3_y");
            choice_sv4.x = PlayerPrefs.GetFloat("choice_sv4_x");
            choice_sv4.y = PlayerPrefs.GetFloat("choice_sv4_y");
            
            choice_b1.transform.position = choice_sv1;
            choice_b2.transform.position = choice_sv2;
            choice_b3.transform.position = choice_sv3;
            choice_b4.transform.position = choice_sv4;
            // 
            if (PlayerPrefs.GetInt("image") == 1)
            {
                image.gameObject.SetActive(true);
            }
            else
            {
                image.gameObject.SetActive(false);
            }

            if (PlayerPrefs.GetInt("image_text") == 1) 
            {
                likesImage.gameObject.SetActive(true);
            }
            else
            {
                likesImage.gameObject.SetActive(false);
            }

            if (PlayerPrefs.GetInt("bool_choice_1") == 1)
            {
                choice_b1.gameObject.SetActive(true);
            }
            else
            {
                choice_b1.gameObject.SetActive(false);
            }

            if (PlayerPrefs.GetInt("bool_choice_2") == 1)
            {
                choice_b2.gameObject.SetActive(true);
            }
            else
            {
                choice_b2.gameObject.SetActive(false);
            }

            if (PlayerPrefs.GetInt("bool_choice_3") == 1)
            {
                choice_b3.gameObject.SetActive(true);
            }
            else
            {
                choice_b3.gameObject.SetActive(false);
            }

            if (PlayerPrefs.GetInt("bool_choice_4") == 1)
            {
                choice_b4.gameObject.SetActive(true);
            }
            else
            {
                choice_b4.gameObject.SetActive(false);
            }


            if (PlayerPrefs.HasKey("image_text") == true)
            {
                if (PlayerPrefs.GetInt("image_text") == 1)
                {
                    bool_likes_image = true;
                }
                else
                {
                    bool_likes_image = false;
                }
            }


            //
            if (PlayerPrefs.HasKey("image_count_likes_0") == false || PlayerPrefs.HasKey("ability_to_put_like_0") == false)
            { // нет смысла проверять каждый элемент массива 
                
                for (int i = 0; i < 4; i++)
                {
                    countLikesImage[i] = Random.Range(12512, 750014);
                    
                    boolLikesImage[i] = true;
                    
                }
            }
            /*
            */
            Lvl(ref tagLvl);
        } else
        {
            
            
            for (int i = 0; i < 4; i++)
            {
                countLikesImage[i] = Random.Range(12512, 750014);
                boolLikesImage[i] = true;
            }
            tagLvl = 1;
            countMeaning = 100;
            countDeaths = 0;
            Lvl(ref tagLvl);
        }

        current_level.text = "Current Level: " + tagLvl.ToString();
        meaning.text = "Meaning: " + countMeaning.ToString();
        deaths.text = "Deaths: " + countDeaths.ToString();


        

        if (Screen.fullScreen == true)
        {
            screen_resolution.text = "Screen Resolution: " + Screen.currentResolution.ToString();
        } else
        {
            screen_resolution.text = "Screen Resolution: " + Screen.width.ToString() + " x " + Screen.height.ToString();
        }


        
    }   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape") || Input.GetKey("a"))
        {
            Application.Quit();
        }
        if (Input.GetKey("s"))
        {
            SaveGame(ref tagLvl);
        }
        if (Input.GetKeyUp("r"))
        {
            for (int i = 0; i < 4; i++)
            {
                countLikesImage[i] = Random.Range(12512, 750014);
                boolLikesImage[i] = true;
            }
            tagLvl = 1;
            countMeaning = 100;
            countDeaths = 0;
            Lvl(ref tagLvl);
        }
        if (Input.GetKeyUp("d"))
        {
            PlayerPrefs.DeleteAll();
            for (int i = 0; i < 4; i++)
            {
                countLikesImage[i] = Random.Range(12512, 750014);
                boolLikesImage[i] = true;
            }
            tagLvl = 1;
            countMeaning = 100;
            countDeaths = 0;
            Lvl(ref tagLvl);
        }
    }

    void LateUpdate () {
        current_level.text = "Current Level: " + tagLvl.ToString();
        meaning.text = "Meaning: " + countMeaning.ToString();
        deaths.text = "Deaths: " + countDeaths.ToString();

    }

    void Lvl(ref int tagLvl)
    {
        switch (tagLvl)
        {
            case 1:
                choice_b1.transform.position = choice_v1;
                choice_b2.transform.position = choice_v2;
                choice_b3.transform.position = choice_v3;
                choice_b4.transform.position = choice_v4;

                image.gameObject.SetActive(false);
                bool_image = false;
                choice_b4.gameObject.SetActive(false);
                bool_choice_4 = false;
                choice_b1.gameObject.SetActive(true);
                bool_choice_1 = true;
                choice_b2.gameObject.SetActive(true);
                bool_choice_2 = true;
                choice_b3.gameObject.SetActive(true);
                bool_choice_3 = true;
                likesImage.gameObject.SetActive(false);
                bool_likes_image = false;

                choice_v3.x = choice_v3.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);
                choice_b3.transform.position = choice_v3;
                choice_v3.x = choice_v3.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);

                text_game.text = "\nВы находитесь в музее.\n Справа от вас находятся картины эпохи Мезозоя";
                scrollbar_text.value = 1;
                choice_t1.text = "Посмотреть картины";
                choice_t2.text = "Пройти дальше";
                choice_t3.text = "Своровать картину";

                Health.value = 100;

                break;

            // следующая ветвь сделана полностью
            case 11: 
                image.gameObject.SetActive(true);
                bool_image = true;
                image.sprite = Resources.Load<Sprite>("da");
                choice_b4.gameObject.SetActive(true);
                bool_choice_4 = true;
                choice_b2.gameObject.SetActive(false);
                bool_choice_2 = false;

                choice_b3.transform.position = choice_v3;

                likesImage.gameObject.SetActive(true);
                bool_likes_image = true;
                if (boolLikesImage[0] == true)
                {
                    choice_b1.gameObject.SetActive(true);
                    bool_choice_1 = true;
                } else
                {
                    choice_b1.gameObject.SetActive(false);
                    bool_choice_1 = false;

                    choice_v3.y = choice_v3.y + (150 * Screen.height / 1080) - (60 * Screen.height / 1080);
                    choice_b3.transform.position = choice_v3;
                    choice_v3.y = choice_v3.y - (150 * Screen.height / 1080) + (60 * Screen.height / 1080);

                    choice_v4.y = choice_v4.y + (150 * Screen.height / 1080) - (60 * Screen.height / 1080);
                    choice_b4.transform.position = choice_v4;
                    choice_v4.y = choice_v4.y - (150 * Screen.height / 1080) + (60 * Screen.height / 1080);
                }

                choice_v1.x = choice_v1.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);
                choice_b1.transform.position = choice_v1;
                choice_v1.x = choice_v1.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);


                text_game.text = "\nВы смотрите картины";
                choice_t1.text = "Поставить картине лайк";               
                choice_t3.text = "Смотреть следующую картину";
                choice_t4.text = "Отвернуться от картин";
                likesImage.text = "Количество лайков: " + countLikesImage[0].ToString();
                break;
            case 111:
                boolLikesImage[0] = false;
                countLikesImage[0]++;
                tagLvl = 11;
                goto case 11;                            
            case 113:               
                image.sprite = Resources.Load<Sprite>("da1");
                choice_b2.gameObject.SetActive(true);
                bool_choice_2 = true;

                choice_b2.transform.position = choice_v2;
                choice_b3.transform.position = choice_v3;
                choice_b4.transform.position = choice_v4;

                if (boolLikesImage[1] == true)
                {
                    choice_b1.gameObject.SetActive(true);
                    bool_choice_1 = true;
                }
                else
                {
                    choice_b1.gameObject.SetActive(false);
                    bool_choice_1 = false;

                    choice_v2.x = choice_v2.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);
                    choice_b2.transform.position = choice_v2;
                    choice_v2.x = choice_v2.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);
                }

                choice_b1.transform.position = choice_v1;

                text_game.text = "\nВы смотрите картины";
                choice_t1.text = "Поставить картине лайк";
                choice_t2.text = "Смотреть предыдущую картину";
                choice_t3.text = "Смотреть следующую картину";
                choice_t4.text = "Отвернуться от картин";
                likesImage.text = "Количество лайков: " + countLikesImage[1].ToString();
                break;
            case 114:
                tagLvl = 1;
                goto case 1;

            case 1131:
                boolLikesImage[1] = false;
                countLikesImage[1]++;
                tagLvl = 113;
                goto case 113;
            case 1132:
                tagLvl = 11;
                goto case 11;
            case 1133:
                image.sprite = Resources.Load<Sprite>("da2");
                choice_b3.gameObject.SetActive(true);
                bool_choice_3 = true;

                choice_b2.transform.position = choice_v2;
                choice_b4.transform.position = choice_v4;

                if (boolLikesImage[2] == true)
                {
                    choice_b1.gameObject.SetActive(true);
                    bool_choice_1 = true;
                }
                else
                {
                    choice_b1.gameObject.SetActive(false);
                    bool_choice_1 = false;

                    choice_v2.x = choice_v2.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);
                    choice_b2.transform.position = choice_v2;
                    choice_v2.x = choice_v2.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);
                }

                text_game.text = "\nВы смотрите картины";
                choice_t1.text = "Поставить картине лайк";
                choice_t2.text = "Смотреть предыдущую картину";
                choice_t3.text = "Смотреть следующую картину";
                choice_t4.text = "Отвернуться от картин";
                likesImage.text = "Количество лайков: " + countLikesImage[2].ToString();
                break;
            case 1134:
                tagLvl = 1;
                goto case 1;

            case 11331:
                boolLikesImage[2] = false;
                countLikesImage[2]++;
                tagLvl = 1133;
                goto case 1133;
            case 11332:
                tagLvl = 113;
                goto case 113;
            case 11333:
                image.sprite = Resources.Load<Sprite>("da3");

                choice_b3.gameObject.SetActive(false);
                bool_choice_3 = false;

                choice_b2.transform.position = choice_v2;

                choice_v4.x = choice_v4.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);
                choice_b4.transform.position = choice_v4;
                choice_v4.x = choice_v4.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);

                if (boolLikesImage[3] == true)
                {
                    choice_b1.gameObject.SetActive(true);
                    bool_choice_1 = true;
                }
                else
                {
                    choice_b1.gameObject.SetActive(false);
                    bool_choice_1 = false;

                    choice_v2.x = choice_v2.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);
                    choice_b2.transform.position = choice_v2;
                    choice_v2.x = choice_v2.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);
                }

                text_game.text = "\nВы смотрите картины";
                choice_t1.text = "Поставить картине лайк";
                choice_t2.text = "Смотреть предыдущую картину";               
                choice_t4.text = "Отвернуться от картин";
                likesImage.text = "Количество лайков: " + countLikesImage[3].ToString();
                break;
            case 11334:
                tagLvl = 1;
                goto case 1;

            case 113331:
                boolLikesImage[3] = false;
                countLikesImage[3]++;
                tagLvl = 11333;
                goto case 11333;
            case 113332:
                tagLvl = 1133;
                goto case 1133;
            case 113334:
                tagLvl = 1;
                goto case 1;




            // следующая ветвь ещё не доделана
            case 12:
                choice_b4.gameObject.SetActive(true);
                bool_choice_4 = true;

                choice_b3.transform.position = choice_v3;


                text_game.text = "\n Вы прошли по коридору несколько метров, \nсделали несколько поворотов.\nИ заблудились.\nМесто, где вы оказались слабо освещено, неподалёку валяются веточки";
                choice_t1.text = "Остановиться и звать на помощь";
                choice_t2.text = "Остановиться и попытаться вспомнить обратный путь";
                choice_t3.text = "Идти куда-нибудь, надеясь, что выберусь";
                choice_t4.text = "Достать спички и разжечь костёр на ветках";
                break;

            case 121:  // Доделана
                choice_b2.gameObject.SetActive(true);
                bool_choice_2 = true;
                choice_b1.transform.position = choice_v1;

                text_game.text = "\n Вы начали громко звать о помощи. \nЭто услышил охранник. \nВ руках у него дубинка и настроен он крайне не дружелюбно.";
                choice_t1.text = "Бежать";
                choice_t2.text = "Поздороваться и извиниться";
                choice_t3.text = "Подраться с ним";
                choice_t4.text = "Использовать математику";
                break;
            case 1211: 
                Health.value -= 80;
                countMeaning -= 20;

                text_game.text = "\n Убежать не удалось. Вы споткнулись обо что-то и упали. Было больно. \n Внимание! \nКоличество смысла уменьшилось.\nОхранник куда-то исчез";
                choice_t1.text = "Осмотреться";
                choice_t2.text = "Прилечь на пол и спать";
                choice_t3.text = "Идти куда-нибудь наугад";
                choice_t4.text = "Достать спички и разжечь костёр на ветках";
                tagLvl = 1212;
                break;
            case 1212:               
                text_game.text = "\n Охранник погрозил вам дубинкой и ушёл";
                choice_t1.text = "Осмотреться";
                choice_t2.text = "Прилечь на пол и спать";
                choice_t3.text = "Идти куда-нибудь наугад";
                choice_t4.text = "Достать спички и разжечь костёр на ветках";
                break;
            case 1213:
                image.gameObject.SetActive(false);
                bool_image = false;
                choice_b2.gameObject.SetActive(false);
                bool_choice_2 = false;
                choice_b3.gameObject.SetActive(false);
                bool_choice_3 = false;
                choice_b4.gameObject.SetActive(false);
                bool_choice_4 = false;

                choice_v1.x = choice_v1.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);
                choice_v1.y = choice_v1.y - (150 * Screen.height / 1080) + (60 * Screen.height / 1080);
                choice_b1.transform.position = choice_v1;
                choice_v1.x = choice_v1.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);
                choice_v1.y = choice_v1.y + (150 * Screen.height / 1080) - (60 * Screen.height / 1080);

                text_game.text = "\nЛучшая защита - это нападение!\n Жаль, что в этот раз не сработало\nОхранник хорошенько побил вас дубинкой, вы умерли.";
                choice_t1.text = "Начать игру сначала";

                Health.value = 0;
                break;
            case 12131:
                countDeaths++;
                tagLvl = 1;
                goto case 1;
            case 1214:
                text_game.text = "\nВы начали произносить какое-то магическое заклинание. Охранник в ужасе убежал через потолок";
                choice_t1.text = "Осмотреться";
                choice_t2.text = "Прилечь на пол и спать";
                choice_t3.text = "Идти куда-нибудь наугад";
                choice_t4.text = "Достать спички и разжечь костёр на ветках";
                tagLvl = 1212;
                break;
            case 12121:
                choice_b1.gameObject.SetActive(false);
                bool_choice_1 = false;
                choice_v2.x = choice_v2.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);
                choice_b2.transform.position = choice_v2;
                choice_v2.x = choice_v2.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);

                if ((bool_choice_1 == false) && (bool_choice_2 == false))
                {
                    choice_v3.y = choice_v3.y + (150 * Screen.height / 1080) - (60 * Screen.height / 1080);
                    choice_b3.transform.position = choice_v3;
                    choice_v3.y = choice_v3.y - (150 * Screen.height / 1080) + (60 * Screen.height / 1080);

                    choice_v4.y = choice_v4.y + (150 * Screen.height / 1080) - (60 * Screen.height / 1080);
                    choice_b4.transform.position = choice_v4;
                    choice_v4.y = choice_v4.y - (150 * Screen.height / 1080) + (60 * Screen.height / 1080);
                }


                text_game.text = "\nНа потолке огромная дыра. До неё метров 8";                
                choice_t3.text = "Идти куда-нибудь наугад";
                choice_t4.text = "Достать спички и разжечь костёр на ветках";
                tagLvl = 1212;
                break;
            case 12122:
                choice_b2.gameObject.SetActive(false);
                bool_choice_2 = false;
                choice_b2.transform.position = choice_v2;

                choice_v1.x = choice_v1.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);
                choice_b1.transform.position = choice_v1;
                choice_v1.x = choice_v1.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);

                if ((bool_choice_1 == false) && (bool_choice_2 == false))
                {
                    choice_v3.y = choice_v3.y + (150 * Screen.height / 1080) - (60 * Screen.height / 1080);
                    choice_b3.transform.position = choice_v3;
                    choice_v3.y = choice_v3.y - (150 * Screen.height / 1080) + (60 * Screen.height / 1080);

                    choice_v4.y = choice_v4.y + (150 * Screen.height / 1080) - (60 * Screen.height / 1080);
                    choice_b4.transform.position = choice_v4;
                    choice_v4.y = choice_v4.y - (150 * Screen.height / 1080) + (60 * Screen.height / 1080);
                }

                text_game.text = "\nВы не можете уснуть. Вам кажется, что за вами кто-то наблюдает";
                choice_t3.text = "Идти куда-нибудь наугад";
                choice_t4.text = "Достать спички и разжечь костёр на ветках";
                tagLvl = 1212;
                break;
            case 12123:
                if ((bool_choice_1 == false) && (bool_choice_2 == false))
                {
                    choice_v3.y = choice_v3.y + (150 * Screen.height / 1080) - (60 * Screen.height / 1080);
                    choice_b3.transform.position = choice_v3;
                    choice_v3.y = choice_v3.y - (150 * Screen.height / 1080) + (60 * Screen.height / 1080);

                    choice_v4.y = choice_v4.y + (150 * Screen.height / 1080) - (60 * Screen.height / 1080);
                    choice_b4.transform.position = choice_v4;
                    choice_v4.y = choice_v4.y - (150 * Screen.height / 1080) + (60 * Screen.height / 1080);
                }

                text_game.text = "\nВы долго бродили по коридору и вернулись в то же самое место";
                choice_t3.text = "Идти куда-нибудь наугад";
                choice_t4.text = "Достать спички и разжечь костёр на ветках";
                tagLvl = 1212;
                break;
            case 12124:
                tagLvl = 124;
                goto case 124;

            case 122: // Сделана
                choice_b2.gameObject.SetActive(false);
                bool_choice_2 = false;

                choice_v1.x = choice_v1.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);
                choice_b1.transform.position = choice_v1;
                choice_v1.x = choice_v1.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);
         
                text_game.text = "\n Вы не смогли вспомнить обратный путь";
                break;
            case 1221:
                tagLvl = 121;
                goto case 121;
            case 1223:
                tagLvl = 123;
                goto case 123;
            case 1224:
                tagLvl = 124;
                goto case 124;

            case 123: // сделано
                choice_b2.gameObject.SetActive(true);
                bool_choice_2 = true;
                choice_b1.transform.position = choice_v1;

                choice_b3.gameObject.SetActive(false);
                bool_choice_3 = false;
                choice_b1.gameObject.SetActive(false);
                bool_choice_1 = false;

                choice_b2.transform.position = choice_v2;
                choice_b4.transform.position = choice_v4;

                choice_v4.x = choice_v4.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);
                choice_b4.transform.position = choice_v4;
                choice_v4.x = choice_v4.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);

                choice_v2.x = choice_v2.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);
                choice_b2.transform.position = choice_v2;
                choice_v2.x = choice_v2.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);

                text_game.text = "\n Вы идёте в случайном направление. Выбраться пока что не получилось";
                choice_t2.text = "Идти дальше";
                choice_t4.text = "Вернуться обратно";
                break;
            case 1232:
                tagLvl = 123;
                goto case 123;
            case 1234: // тут со вторым было что-то не так. Вместо 2 была 1. Где true
                choice_b1.gameObject.SetActive(true);
                bool_choice_1 = true;
                choice_b2.gameObject.SetActive(true);
                bool_choice_2 = true;
                choice_b3.gameObject.SetActive(true);
                bool_choice_3 = true;
                choice_b4.gameObject.SetActive(true);
                bool_choice_4 = true;

                choice_b1.transform.position = choice_v1;
                choice_b2.transform.position = choice_v2;
                choice_b3.transform.position = choice_v3;
                choice_b4.transform.position = choice_v4;


                text_game.text = "\n Вы вернулись. Здесь всё так же слабо освещено, неподалёку валяются веточки";
                choice_t1.text = "Остановиться и звать на помощь";
                choice_t2.text = "Остановиться и попытаться вспомнить обратный путь";
                choice_t3.text = "Идти куда-нибудь, надеясь, что выберусь";
                choice_t4.text = "Достать спички и разжечь костёр на ветках";
                break;
            case 12341:
                tagLvl = 121;
                goto case 121;
            case 12342:
                tagLvl = 122;
                goto case 122;
            case 12343:
                tagLvl = 123;
                goto case 123;
            case 12344:
                tagLvl = 124;
                goto case 124;


            case 124: // не сделано !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                choice_b1.gameObject.SetActive(true);
                bool_choice_1 = true;
                choice_b2.gameObject.SetActive(true);
                bool_choice_2 = true;
                choice_b3.gameObject.SetActive(true);
                bool_choice_3 = true;

                choice_b1.transform.position = choice_v1;
                choice_b2.transform.position = choice_v2;
                choice_b3.transform.position = choice_v3;
                choice_b4.transform.position = choice_v4;

                if (countMeaning < 100) {
                    choice_b4.gameObject.SetActive(true);
                    bool_choice_4 = true;
                } else
                {
                    choice_b4.gameObject.SetActive(false);
                    bool_choice_4 = false;
                    choice_v3.x = choice_v3.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);
                    choice_b3.transform.position = choice_v3;
                    choice_v3.x = choice_v3.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);
                }

                text_game.text = "\n Вы разожгли костёр. У дальней стены вы заметили крадущегося человека с картиной";
                choice_t1.text = "Присмотреться к человеку";
                choice_t2.text = "Присмотреться к картине";
                choice_t3.text = "Поздороваться с человеком";
                choice_t4.text = "Поздороваться с картиной"; // Ведёт к 3 ловить вора!
                break;
            case 1241:
                text_game.text = "\n Вы присмотрелись к человеку. Он в чёрной маске, сильно смахивает на вора.\nВнезапно человек бросился бежать от вас";
                tagLvl = 1249;
                goto case 1249;
            case 1242:
                text_game.text = "\n Вы присмотрелись к картине. Похоже, она из музея\nВнезапно человек бросился бежать от вас";
                tagLvl = 1249;
                goto case 1249;
            case 1243:
                text_game.text = "\n Вы поздоровались с человеком\n Он начал от вас убегать";
                tagLvl = 1249;
                goto case 1249;
            case 1244:
                text_game.text = "\n Вы поздоровались с картиной.\nЧеловек начал убегать от вас.\n Очень Быстро";
                tagLvl = 1249;
                goto case 1249;
            case 1249: // Использовал 9 в tagLvl
                choice_b2.gameObject.SetActive(false);
                bool_choice_2 = false;
                choice_b3.gameObject.SetActive(false);
                bool_choice_3 = false;
                choice_b4.gameObject.SetActive(false);
                bool_choice_4 = false;

                choice_v1.x = choice_v1.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);
                choice_v1.y = choice_v1.y - (150 * Screen.height / 1080) + (60 * Screen.height / 1080);
                choice_b1.transform.position = choice_v1;
                choice_v1.x = choice_v1.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);
                choice_v1.y = choice_v1.y + (150 * Screen.height / 1080) - (60 * Screen.height / 1080);

                choice_t1.text = "Догнать!";
                break;
            // следующая ветвь сделана полностью
            case 13:
                text_game.text = "\nКак Вы будете это делать?";
                choice_t1.text = "Тихонечько";
                choice_t2.text = "Быстро";
                choice_t3.text = "Передумать";                
                break;

            case 131:
                text_game.text = "\nВас заметил и схватил охранник. Кажется он очень зол.";
                choice_t1.text = "Ударить охранника";
                choice_t2.text = "Извиниться";
                choice_t3.text = "Вырваться из его объятий и бежать";
                break;
            case 132:
                text_game.text = "\nВы решили быстренько взять картину, но не тут то было!\nОна словно приклеена.";
                choice_t1.text = "Попытаться быстро отодрать картину от стены";
                choice_t2.text = "Попытаться понять, как её снять";
                choice_t3.text = "Передумать";
                break;
            case 133:
                tagLvl = 1;                
                goto case 1;

            case 1311:
            case 1312:
            case 1313:
            case 1321:
            case 1322:
            case 1323:
                image.gameObject.SetActive(false);
                bool_image = false;
                choice_b2.gameObject.SetActive(false);
                bool_choice_2 = false;
                choice_b3.gameObject.SetActive(false);
                bool_choice_3 = false;
                choice_b4.gameObject.SetActive(false);
                bool_choice_4 = false;

                choice_v1.x = choice_v1.x + (680 * Screen.width / 1920) - (250 * Screen.width / 1920);
                choice_v1.y = choice_v1.y - (150 * Screen.height / 1080) + (60 * Screen.height / 1080);
                choice_b1.transform.position = choice_v1;
                choice_v1.x = choice_v1.x - (680 * Screen.width / 1920) + (250 * Screen.width / 1920);
                choice_v1.y = choice_v1.y + (150 * Screen.height / 1080) - (60 * Screen.height / 1080);

                text_game.text = "\nВы попытались что-либо сделать, но не успели. \nОхранник пырнул вас ножом, вы умерли.";
                choice_t1.text = "Начать игру сначала";

                Health.value = 0;
                break;

            case 13111:
            case 13121:
            case 13131:
            case 13211:
            case 13221:
            case 13231:
                countDeaths++;
                tagLvl = 1;               
                goto case 1;

            default:
                image.gameObject.SetActive(false);
                bool_image = false;
                text_game.text = "\nКвест ещё не доделан, ждите";
                choice_t1.text = "Error";
                choice_t2.text = "Error";
                choice_t3.text = "Error";
                choice_t4.text = "Error";                
                break;
        }
    }

    public void LvlChange(int new_choice)
    {
        tagLvl = tagLvl * 10 + new_choice;
        Lvl(ref tagLvl);
    }

    public void SaveGame(ref int tagLvl)
    {
        choice_sv1 = choice_b1.transform.position;
        choice_sv2 = choice_b2.transform.position;
        choice_sv3 = choice_b3.transform.position;
        choice_sv4 = choice_b4.transform.position;

        PlayerPrefs.SetInt("Save", 1);
        PlayerPrefs.SetInt("Lvl", tagLvl);
        PlayerPrefs.SetInt("Meaning", countMeaning);
        PlayerPrefs.SetInt("Deaths", countDeaths);
        PlayerPrefs.SetFloat("Health", Health.value);

        for (int i = 0; i < 4; i++) 
        {
            PlayerPrefs.SetInt("image_count_likes_" + i.ToString(), countLikesImage[i]);
            if (boolLikesImage[i] == true)
            {
                PlayerPrefs.SetInt("ability_to_put_like_" + i.ToString(), 1);
            } else
            {
                PlayerPrefs.SetInt("ability_to_put_like_" + i.ToString(), 0);
            }
        }

        PlayerPrefs.SetFloat("choice_sv1_x", choice_sv1.x);
        PlayerPrefs.SetFloat("choice_sv1_y", choice_sv1.y);

        PlayerPrefs.SetFloat("choice_sv2_x", choice_sv2.x);
        PlayerPrefs.SetFloat("choice_sv2_y", choice_sv2.y);

        PlayerPrefs.SetFloat("choice_sv3_x", choice_sv3.x);
        PlayerPrefs.SetFloat("choice_sv3_y", choice_sv3.y);

        PlayerPrefs.SetFloat("choice_sv4_x", choice_sv4.x);
        PlayerPrefs.SetFloat("choice_sv4_y", choice_sv4.y);

        if (bool_image == true)
        {
            PlayerPrefs.SetInt("image", 1);
        } else
        {
            PlayerPrefs.SetInt("image", 0);
        }

        if (bool_likes_image == true) //
        {
            PlayerPrefs.SetInt("image_text", 1);
        }
        else
        {
            PlayerPrefs.SetInt("image_text", 0);
        }

        if (bool_choice_1 == true)
        {
            PlayerPrefs.SetInt("bool_choice_1", 1);
        }
        else
        {
            PlayerPrefs.SetInt("bool_choice_1", 0);
        }

        if (bool_choice_2 == true)
        {
            PlayerPrefs.SetInt("bool_choice_2", 1);
        }
        else
        {
            PlayerPrefs.SetInt("bool_choice_2", 0);
        }

        if (bool_choice_3 == true)
        {
            PlayerPrefs.SetInt("bool_choice_3", 1);
        }
        else
        {
            PlayerPrefs.SetInt("bool_choice_3", 0);
        }

        if (bool_choice_4 == true)
        {
            PlayerPrefs.SetInt("bool_choice_4", 1);
        }
        else
        {
            PlayerPrefs.SetInt("bool_choice_4", 0);
        }

        


    }

    
}
