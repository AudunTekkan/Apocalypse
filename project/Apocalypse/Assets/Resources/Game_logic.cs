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
    public Text textMain_t;
    public Text lvl1212;
    public Text lvl12491;



    public Text textTimer;

    public Button choice_b1;
    public Button choice_b2;
    public Button choice_b3;
    public Button choice_b4;
    public Button turnLeft_b;
    public Button turnRight_b;
    public Button jump_b;
    public Button stoop_b;

    public Image image;
    public Image robberImage_i;
    public Image cautionImageLeft_i;
    public Image cautionImageRight_i;
    public Image obstacleImage1_i;
    public Image obstacleImage2_i;

    public Slider Health;
    public GameObject Pursuit_Panel;
    public GameObject Story_Pannel;

    private Vector2 textMain_v;
 
    
    private Vector2 turnLeft_v;
    private Vector2 turnRight_v;
    private Vector2 jump_v;
    private Vector2 stoop_v;
    private Vector2 cautionImageLeft_v;
    private Vector2 cautionImageRight_v;
    private Vector2 obstacleImage1_v;
    private Vector2 obstacleImage2_v;
    private Vector2 robberImage_v;
    // Для хранения первоначальной позиции кнопок.
    private Vector2 choice_v1; // После изменения значений этих векторов (choice_v№) сразу же присваиваем их первоначальное значение обратно
    private Vector2 choice_v2;
    private Vector2 choice_v3;
    private Vector2 choice_v4;

    // Для передачи текущей позиции кнопок в SaveGame.  
    private Vector2 choice_sv1;
    private Vector2 choice_sv2;
    private Vector2 choice_sv3;
    private Vector2 choice_sv4;

    private int tagLvl; 
    private int countMeaning;
    private int countDeaths;
    private int countSeconds;
    private int frame;
    private int frame2;
    private int oneSecond;
    private int pursuitEvents;
    private int valueCountSeconds;
    private int valueOneSecond;
    private int pursuitDelay;

    private int[] countLikesImage = new int [4]; // счётчик лайков для картин 

    private bool bool_Pursuit_Panel;
    private bool bool_frame2;
    private bool bool_damage; 
    private bool bool_image; /* при указании: ...gameObject.SetActive(...) обязательно присваивать этим переменным соответсвующие логические значения
    Они нужны, чтобы работать с тем фактом: активны ли эти компоненты?  Т.е. для передачи состояния активности компонента в SaveGame. */
    private bool bool_choice_1;
    private bool bool_choice_2;
    private bool bool_choice_3;
    private bool bool_choice_4;
    private bool bool_likes_image; 

    private bool[] boolLikesImage = new bool [4]; //Для проверки: есть ли у игрока возможность поставить лайк картине 


    void Start () {
        choice_v1 = choice_b1.transform.position;
        choice_v2 = choice_b2.transform.position;
        choice_v3 = choice_b3.transform.position;
        choice_v4 = choice_b4.transform.position;

        textMain_v = textMain_t.transform.position;
      
        turnLeft_v = turnLeft_b.transform.position;
        turnRight_v = turnRight_b.transform.position;
        jump_v = jump_b.transform.position;
        stoop_v = stoop_b.transform.position;
        cautionImageLeft_v = cautionImageLeft_i.transform.position;
        cautionImageRight_v = cautionImageLeft_i.transform.position;
        obstacleImage1_v = obstacleImage1_i.transform.position;
        obstacleImage2_v = obstacleImage2_i.transform.position;
        robberImage_v = robberImage_i.transform.position;
        Pursuit_Panel.gameObject.SetActive(false);
        Story_Pannel.gameObject.SetActive(true);
        valueCountSeconds = 86;
        countSeconds = valueCountSeconds;
        frame = 0;
        frame2 = -42;
        valueOneSecond = 45;
        oneSecond = valueOneSecond;
        pursuitEvents = 0;
        pursuitDelay = 180; // есть ещё и на tagLvl = 12491
        bool_frame2 = true;
        bool_damage = true;



        // Если есть сохранение, достаём сохранённые значения
        if (PlayerPrefs.HasKey("Save")) 
        {
            tagLvl = PlayerPrefs.GetInt("Lvl");
            countMeaning = PlayerPrefs.GetInt("Meaning");
            countDeaths = PlayerPrefs.GetInt("Deaths");
            Health.value = PlayerPrefs.GetFloat("Health");
            // чтобы следующий блок кода работал, необходимо существование image_count_likes_0 и ability_to_put_like_0. То есть необходимо иметь хотя бы одно сохранение. 
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
                      
            Lvl(ref tagLvl); // переход к уровню из сохранения
            
            
            // если сохранения нет, работает следующий блок кода
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
            Lvl(ref tagLvl); // переход к уровню 1
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

    void Update()
    {
        if (Input.GetKey("escape") || Input.GetKey("a"))
        {
            Application.Quit();
        }
        if (Input.GetKey("s")) // вызывает сохранение 
        {
            SaveGame(ref tagLvl);
        }
        if (Input.GetKeyUp("r")) // Сброс текущего прогресса, переход на первый уровень. Сохранение остаётся
        {
            for (int i = 0; i < 4; i++)
            {
                countLikesImage[i] = Random.Range(12512, 750014);
                boolLikesImage[i] = true;
            }
            tagLvl = 1;
            countMeaning = 100;
            countDeaths = 0;
            Story_Pannel.gameObject.SetActive(true);
            Pursuit_Panel.gameObject.SetActive(false);
            countSeconds = valueCountSeconds;
            frame = 0;
            frame2 = -42;
            oneSecond = valueOneSecond;
            pursuitEvents = 0;
            bool_frame2 = true;
            bool_damage = true;
            bool_Pursuit_Panel = false;
            Lvl(ref tagLvl);
        }
        if (Input.GetKeyUp("d")) // Сброс текущего прогресса, переход на первый уровень. Сохранение НЕ остаётся
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
            Story_Pannel.gameObject.SetActive(true);
            Pursuit_Panel.gameObject.SetActive(false);
            countSeconds = valueCountSeconds;
            frame = 0;
            frame2 = -42;
            oneSecond = valueOneSecond;
            pursuitEvents = 0;
            bool_frame2 = true;
            bool_damage = true;
            bool_Pursuit_Panel = false;
            Lvl(ref tagLvl);
        }
    }

    void LateUpdate () {
        current_level.text = "Current Level: " + tagLvl.ToString();
        meaning.text = "Meaning: " + countMeaning.ToString();
        deaths.text = "Deaths: " + countDeaths.ToString();
        if (bool_Pursuit_Panel == true)
        {
            textTimer.text = countSeconds.ToString();
            if (countSeconds > 0 && frame == 60) {
                countSeconds -= 1;
                frame = 0;
            }
            frame += 1;
            if (pursuitDelay > 0)
            {
                pursuitDelay -= 1;
            } else
            {
                if (bool_frame2 == false)
                {
                    oneSecond -= 1;
                    if (oneSecond == 0)
                    {
                        bool_frame2 = true;

                        if (pursuitEvents == 1)
                        {
                            cautionImageRight_i.gameObject.SetActive(false);
                            if (bool_damage == true)
                            {
                                lvl12491.text = "hitTheWall";
                                Health.value -= 15;
                            }
                        }
                        else if (pursuitEvents == 2)
                        {
                            obstacleImage2_i.gameObject.SetActive(false);
                            if (bool_damage == true)
                            {
                                lvl12491.text = "toHurtYourselfOnTheShardsOfGlass";
                                Health.value -= 16;
                            }
                        }
                        else if (pursuitEvents == 3)
                        {
                            obstacleImage2_i.gameObject.SetActive(false);
                            if (bool_damage == true)
                            {
                                lvl12491.text = "spikes";
                                Health.value -= 90;
                            }
                        }
                        else if (pursuitEvents == 4)
                        {
                            obstacleImage2_i.gameObject.SetActive(false);
                            if (bool_damage == true)
                            {
                                lvl12491.text = "slip";
                                Health.value -= 4;
                            }
                        }
                        else if (pursuitEvents == 5)
                        {
                            obstacleImage1_i.gameObject.SetActive(false);
                            if (bool_damage == true)
                            {
                                lvl12491.text = "bananaHit";
                                Health.value -= 10;
                            }
                        }
                        else if (pursuitEvents == 6)
                        {
                            obstacleImage1_i.gameObject.SetActive(false);
                            if (bool_damage == true)
                            {
                                lvl12491.text = "cryBird";
                                Health.value -= 22;
                            }
                        }
                        else if (pursuitEvents == 7)
                        {
                            obstacleImage1_i.gameObject.SetActive(false);
                            if (bool_damage == true)
                            {
                                lvl12491.text = "explosion";
                                Health.value -= 66;
                            }
                        }
                        else if (pursuitEvents == 8)
                        {
                            cautionImageLeft_i.gameObject.SetActive(false);
                            if (bool_damage == true)
                            {
                                lvl12491.text = "hitTheWall";
                                Health.value -= 15;
                            }
                        }
                        pursuitEvents = 0;
                        bool_damage = true;
                    }
                }
                else
                {
                    if (frame2 == -42)
                    {
                        frame2 = Random.Range(20, 90); // время до появления новых препятствий (в кадрах)
                    }
                    if (frame2 > 0)
                    {
                        frame2 -= 1;
                    }
                    else
                    {
                        frame2 = -42;
                        oneSecond = valueOneSecond;
                        bool_frame2 = false;
                        pursuitEvents = Random.Range(1, 9); // 3 - снизу, 3 - сверху, 1 - слева, 1 - справа
                        if (pursuitEvents == 1)
                        {
                            cautionImageRight_i.gameObject.SetActive(true);
                            lvl12491.text = "turn";
                        }
                        else if (pursuitEvents == 2)
                        {
                            obstacleImage2_i.sprite = Resources.Load<Sprite>("разбитая бутылка");
                            obstacleImage2_i.gameObject.SetActive(true);
                        }
                        else if (pursuitEvents == 3)
                        {
                            obstacleImage2_i.sprite = Resources.Load<Sprite>("Шипы");
                            obstacleImage2_i.gameObject.SetActive(true);
                        }
                        else if (pursuitEvents == 4)
                        {
                            obstacleImage2_i.sprite = Resources.Load<Sprite>("кожура банана");
                            obstacleImage2_i.gameObject.SetActive(true);
                        }
                        else if (pursuitEvents == 5)
                        {
                            obstacleImage1_i.sprite = Resources.Load<Sprite>("Банан");
                            obstacleImage1_i.gameObject.SetActive(true);
                            lvl12491.text = "bananaFlies";
                        }
                        else if (pursuitEvents == 6)
                        {
                            obstacleImage1_i.sprite = Resources.Load<Sprite>("Птица");
                            obstacleImage1_i.gameObject.SetActive(true);
                            lvl12491.text = "birdFlies";
                        }
                        else if (pursuitEvents == 7)
                        {
                            obstacleImage1_i.sprite = Resources.Load<Sprite>("бомба");
                            obstacleImage1_i.gameObject.SetActive(true);
                            lvl12491.text = "bombHissings";
                        }
                        else if (pursuitEvents == 8)
                        {
                            cautionImageLeft_i.gameObject.SetActive(true);
                            lvl12491.text = "turn";
                        }
                    }
                }

            }
            if (Health.value <= 0)
            {
                // смерть
                tagLvl = 666;
                Lvl(ref tagLvl);
            }
            if (countSeconds == 0)
            {
                tagLvl = 124915;
                Lvl(ref tagLvl);
            }
            if ((countSeconds < 5) && (pursuitDelay <= 0) && (frame2 != -42))
            {
                pursuitDelay = 300;
            }
        }
    }

    void Lvl(ref int tagLvl) // отвечает за расположение и содержание элементов на уровнях. 
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
                choice_t1.text = "Посмотреть картины";
                choice_t2.text = "Пройти дальше";
                choice_t3.text = "Своровать картину";

                Health.value = 100;

                break;
            
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

            case 121:
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
                lvl1212.text = "hit the floor";

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
                lvl1212.text = "power of mathematics";
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

            case 122:
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

            case 123: 
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
            case 1234:
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


            case 124:
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
                choice_t4.text = "Поздороваться с картиной"; 
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
            case 12491:// Погоня за вором
                // 
                choice_b1.gameObject.SetActive(false);
                bool_choice_1 = false;
                text_game.text = "";
                Story_Pannel.gameObject.SetActive(false);
                pursuitDelay = 180; 
                // расставляем элементы погони в начальное положение, затем активируем панель погони 
                cautionImageRight_v.x = cautionImageRight_v.x + (340 * Screen.width / 1920);
                cautionImageRight_i.transform.position = cautionImageRight_v;
                cautionImageRight_v.x = cautionImageRight_v.x - (340 * Screen.width / 1920);
                cautionImageRight_i.gameObject.SetActive(false);

                cautionImageLeft_v.x = cautionImageLeft_v.x - (340 * Screen.width / 1920);
                cautionImageLeft_i.transform.position = cautionImageLeft_v;
                cautionImageLeft_v.x = cautionImageLeft_v.x + (340 * Screen.width / 1920);
                cautionImageLeft_i.gameObject.SetActive(false);

                obstacleImage1_v.y = obstacleImage1_v.y - (455 * Screen.height / 1080);
                obstacleImage1_i.transform.position = obstacleImage1_v;
                obstacleImage1_v.y = obstacleImage1_v.y + (455 * Screen.height / 1080);
                obstacleImage1_i.gameObject.SetActive(false);

                obstacleImage2_v.y = obstacleImage2_v.y - (885 * Screen.height / 1080);
                obstacleImage2_i.transform.position = obstacleImage2_v;
                obstacleImage2_v.y = obstacleImage2_v.y + (885 * Screen.height / 1080);
                obstacleImage2_i.gameObject.SetActive(false);

                robberImage_v.y = robberImage_v.y - (60 * Screen.height / 1080);
                robberImage_i.transform.position = robberImage_v;
                robberImage_v.y = robberImage_v.y + (60 * Screen.height / 1080);

                textMain_v.y = textMain_v.y - (415 * Screen.height / 1080);
                textMain_t.transform.position = textMain_v;
                textMain_v.y = textMain_v.y + (415 * Screen.height / 1080);
                textMain_t.text = "Вор убегает!";

                

                

                stoop_v.y = stoop_v.y - (860 * Screen.height / 1080) + (95 * Screen.height / 1080);
                stoop_b.transform.position = stoop_v;
                stoop_v.y = stoop_v.y + (860 * Screen.height / 1080) - (95 * Screen.height / 1080);

                jump_v.y = jump_v.y - (860 * Screen.height / 1080) + (215 * Screen.height / 1080);
                jump_b.transform.position = jump_v;
                jump_v.y = jump_v.y + (860 * Screen.height / 1080) - (215 * Screen.height / 1080);

                 turnLeft_v.x = turnLeft_v.x + (200 * Screen.width / 1920);
                 turnLeft_b.transform.position = turnLeft_v;
                 turnLeft_v.x = turnLeft_v.x - (200 * Screen.width / 1920);

                 turnRight_v.x = turnRight_v.x - (200 * Screen.width / 1920);
                 turnRight_b.transform.position = turnRight_v;
                 turnRight_v.x = turnRight_v.x + (200 * Screen.width / 1920);

                Pursuit_Panel.gameObject.SetActive(true);
                bool_Pursuit_Panel = true;
                break;

            case 666:
                Story_Pannel.gameObject.SetActive(true);
                Pursuit_Panel.gameObject.SetActive(false);
                countSeconds = valueCountSeconds;
                frame = 0;
                frame2 = -42;
                oneSecond = valueOneSecond;
                pursuitEvents = 0;
                bool_frame2 = true;
                bool_damage = true;
                bool_Pursuit_Panel = false;

                image.gameObject.SetActive(false);
                bool_image = false;
                choice_b1.gameObject.SetActive(true);
                bool_choice_1 = true;
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

                text_game.text = "\nВы умерли!";
                choice_t1.text = "Начать игру сначала";
                break;
            case 6661:
                countDeaths++;
                tagLvl = 1;
                goto case 1;

            case 124915:
                Story_Pannel.gameObject.SetActive(true);
                Pursuit_Panel.gameObject.SetActive(false);
                countSeconds = valueCountSeconds;
                frame = 0;
                frame2 = -42;
                oneSecond = valueOneSecond;
                pursuitEvents = 0;
                bool_frame2 = true;
                bool_damage = true;
                bool_Pursuit_Panel = false;

                image.gameObject.SetActive(true);
                bool_image = true;
                image.sprite = Resources.Load<Sprite>("мезозой");
                choice_b1.gameObject.SetActive(true);
                bool_choice_1 = true;
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

                text_game.text = "\n Конец\n\n\nВы поймали вора и спасли самую ценную картину музея!";
                choice_t1.text = "выйти из игры";
                break;
            case 1249151:
                Application.Quit();
                break;

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



    public void LvlChange(int new_choice) // вызывается при нажатие по кнопке. 
    {
        tagLvl = tagLvl * 10 + new_choice; // new_choice соответствует номеру кнопки на сцене 
        Lvl(ref tagLvl);
    }

    public void PursuitButtons(int PursuitChoice)
    {
        if (PursuitChoice == 1)
        {
            if (pursuitEvents == 8)
            {
                bool_damage = false;
                oneSecond = 1;
            } else if (pursuitEvents == 0)
            {
                Health.value -= 15;
            } else
            {
                bool_damage = true;
                oneSecond = 1;
            }
        }
        else if (PursuitChoice == 2)
        {
            if ((pursuitEvents == 2) || (pursuitEvents == 3) || (pursuitEvents == 4))
            {
                bool_damage = false;
                oneSecond = 1;
            } else if (pursuitEvents == 0)
            {

            } else
            {
                bool_damage = true;
                oneSecond = 1;
            }
        }
        else if (PursuitChoice == 3)
        {
            if (pursuitEvents == 1)
            {
                bool_damage = false;
                oneSecond = 1;
            } else if (pursuitEvents == 0)
            {
                Health.value -= 15;
            } else
            {
                bool_damage = true;
                oneSecond = 1;
            }
        }
        else if (PursuitChoice == 4)
        {
            if ((pursuitEvents == 5) || (pursuitEvents == 6) || (pursuitEvents == 7))
            {
                bool_damage = false;
                oneSecond = 1;
            } else if (pursuitEvents == 0)
            {

            } else
            {
                bool_damage = true;
                oneSecond = 1;
            }
        }
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
