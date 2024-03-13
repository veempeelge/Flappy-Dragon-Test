using UnityEngine;
using System.Collections;
using SgLib;
using System;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static event System.Action PlayerDied;

    [Header("Gameplay Config")]

    public float maxVelocity;
    public float jumpForce = 8f;
    //How high player jump
    public float sideForce = .8f;
   //side
    public float rotateAngle = 50f;
    //Rotate angle of player
    [HideInInspector]
    public bool hasStarted = false;
    [HideInInspector]
    public bool hitObstacle;
    [HideInInspector]
    public bool playerFallDown = false;
    [HideInInspector]
    public bool hasHitGround = false;

    [Header("Gameplay Preferences")]
    public GameManager gameManager;
    public GameObject player;
   // public GameObject flyObj, dieObj;
    public ParticleSystem goldParticlePrefab;
    public ParticleSystem staminaParticlePrefab;
    public ParticleSystem hitGroundParticlePrefab;
    public ParticleSystem boostParticlePrefab;
    public ParticleSystem boostingParticle;
    public AnimationClip jump;
    public AnimationClip rotate;
    public Color gray;
    public GameObject tutorialRight;
    public GameObject tutorialLeft;
      
    private Rigidbody rigid;
    [SerializeField] private Animator anim;
    private int turn = 1;
    private bool isFinishRotate = false;
    private CameraController cameraController;

    private Vector3 playerPosition;

    float clickCount;
    bool flip;

    public float stamina;
    float maxStamina;
    float cooldown;
    float regenCooldown = .5f;
    float boostTimer;
    [SerializeField] Image staminaBar;
    bool canCollide = true;
    int flapCount;

    [SerializeField] CoinScore CoinScore;

    [SerializeField] parallax parallax;

    [SerializeField] int lastLevel = 3;

    void OnEnable()
    {
        GameManager.GameStateChanged += GameManager_GameStateChanged;
    }

    void OnDisable()
    {
        GameManager.GameStateChanged -= GameManager_GameStateChanged;
    }

    void GameManager_GameStateChanged(GameState newState, GameState oldState)
    {
        if (newState == GameState.Playing && oldState == GameState.Prepare)
        {
            if (!hasStarted)
            {
                hasStarted = true;
            }

            //Flap();
        }
    }

    // Use this for initialization
    void Start()
    {
        stamina = PlayerPrefs.GetFloat("Stamina");
        maxStamina = PlayerPrefs.GetFloat("Stamina");
        cameraController = Camera.main.GetComponent<CameraController>();
        Debug.Log(maxStamina);

        // Change the character to the selected one
        GameObject currentCharacter = CharacterManager.Instance.characters[CharacterManager.Instance.CurrentCharacterIndex];
        if (currentCharacter.transform.childCount > 0)
        {
            Mesh charMainMesh = currentCharacter.transform.Find("Main").GetComponent<MeshFilter>().sharedMesh;
            Mesh charRightWingMesh = currentCharacter.transform.Find("RightWing").GetComponent<MeshFilter>().sharedMesh;
            Mesh charLeftWingMesh = currentCharacter.transform.Find("LeftWing").GetComponent<MeshFilter>().sharedMesh;
            Material charMaterial = currentCharacter.transform.Find("Main").GetComponent<Renderer>().sharedMaterial;

            GameObject playerMain = player.transform.Find("Main").gameObject;
            GameObject playerRightWing = player.transform.Find("RightWing").gameObject;
            GameObject playerLeftWing = player.transform.Find("LeftWing").gameObject;

            playerMain.GetComponent<MeshFilter>().mesh = charMainMesh;
            playerMain.GetComponent<Renderer>().material = charMaterial;

            playerRightWing.GetComponent<MeshFilter>().mesh = charRightWingMesh;
            playerRightWing.GetComponent<Renderer>().material = charMaterial;

            playerLeftWing.GetComponent<MeshFilter>().mesh = charLeftWingMesh;
            playerLeftWing.GetComponent<Renderer>().material = charMaterial;
        }
        else
        {
            Mesh charMesh = currentCharacter.GetComponent<MeshFilter>().sharedMesh;
            Material charMaterial = currentCharacter.GetComponent<Renderer>().sharedMaterial;
            GameObject main = player.transform.Find("Main").gameObject;

            main.GetComponent<MeshFilter>().mesh = charMesh;
            main.GetComponent<Renderer>().material = charMaterial;
        }
        rigid = GetComponent<Rigidbody>();
        playerPosition = player.transform.position;
    }
	
    // Update is called once per frame
    void Update()
    {
        
        staminaBar.fillAmount = stamina / maxStamina;
        //Check player fall out of camera
        if (Camera.main.WorldToScreenPoint(transform.position).y < -30 && GameManager.Instance.GameState != GameState.GameOver)
        {
            playerFallDown = true;
            Die();
        }

       if ((Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetMouseButtonDown(0)) && GameManager.Instance.GameState == GameState.Playing)
        {

            //Flap();
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetMouseButtonDown(0)) && stamina > 0f)
            {
                Flap();
                rigid.useGravity = false;
                
                stamina -= .1f;
                cooldown = 0f;

                if (rigid.velocity.y < 0)
                {
                    rigid.velocity = new Vector3(0, 0, 0);
                }
                //if (clickCount == 0)
                //{
                //    flip = true;
                //    clickCount++;
                //}
                //else
                //{
                //    flip = false;
                //    clickCount--;
                //}
            }


            

            //if (Input.GetKey(KeyCode.A) || Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width / 2)
            //{
            //    MoveLeft();
            //    //        if (flip)
            //    //        {
            //    //            MoveLeft();
            //    //        }
            //    //        else if (!flip)
            //    //        {
            //    //            MoveRight();
            //    //        }
            //}

            //if (Input.GetKey(KeyCode.A) || Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width / 2)
            //{
            //    MoveRight();
            //}
            rigid.useGravity = true;


        }

      

       // if ((Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.A)) && GameManager.Instance.GameState == GameState.Playing)
        {
           // Flap();
          //  Debug.Log("Should be flapping");
        }


        if ((Input.GetKey(KeyCode.D) || Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width / 2) && GameManager.Instance.GameState == GameState.Playing)
        {
            MoveRight();
            tutorialLeft.SetActive(false);
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width / 2 ) && GameManager.Instance.GameState == GameState.Playing)
        {
            MoveLeft();
            tutorialRight.SetActive(false);
        }


        // Fix position
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void FixedUpdate()
    {
        string staminaTxt = stamina.ToString();
        cooldown += 1f * Time.deltaTime;
        
        boostTimer += 1f*Time.deltaTime;

        if (cooldown > regenCooldown && stamina < maxStamina)
        {
            stamina += .1f;
        }

        if (boostTimer >= 3f && !canCollide)
        {
            canCollide = true;
            jumpForce -= 500;
            boostingParticle.Stop();
            stamina = maxStamina;
        }

        if (rigid.position.x > 4.538022f)
        {
            rigid.velocity = new Vector3(-sideForce, rigid.velocity.y, 0);
        }

        if (rigid.position.x < -4.538022f)
        {
            rigid.velocity = new Vector3(sideForce, rigid.velocity.y, 0);
        }

      

    }

    
    void StopMove()
    {
        rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
    }
    private void MoveLeft()
    {
 
     //   SoundManager.Instance.PlaySound(SoundManager.Instance.flap);
        StartCoroutine(MoveSideForPlayerLeft()); //add velocity for player
        if (!isFinishRotate)
        {
            isFinishRotate = true;
           StartCoroutine(RotateParentPlayer()); //rotate player
        }
    }

    private void MoveRight()
    {
      //  SoundManager.Instance.PlaySound(SoundManager.Instance.flap);
        StartCoroutine(MoveSideForPlayerRight()); //add velocity for player
        if (!isFinishRotate)
        {
            isFinishRotate = true;
           StartCoroutine(RotateParentPlayer()); //rotate player
        }
    }

    IEnumerator MoveSideForPlayerRight()
    {
        yield return new WaitForFixedUpdate();
        rigid.velocity = new Vector3(sideForce, rigid.velocity.y, 0);
       
    }

    IEnumerator MoveSideForPlayerLeft()
    {
        yield return new WaitForFixedUpdate();
        rigid.velocity = new Vector3(-sideForce, rigid.velocity.y, 0);
 
    }
    IEnumerator AddVelocityForPlayer()
    {

        yield return new WaitForFixedUpdate();
        rigid.AddForce(rigid.velocity.x, jumpForce, 0);
        if (rigid.velocity.y > maxVelocity)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, maxVelocity - 1, 0);
        }
    }

    void Flap()
    {
        parallax.FlapUP();
        if (flapCount == 5)
        {
            SoundManager.Instance.ResetPitch();
            flapCount = 0;
        }
        flapCount++;
        SoundManager.Instance.PlaySoundFlap(SoundManager.Instance.flap);
        SoundManager.Instance.ChangePitch();
        StartCoroutine(AddVelocityForPlayer()); //add velocity for player
        if (!isFinishRotate)
        {
            isFinishRotate = true;
            StartCoroutine(RotateParentPlayer()); //rotate player
        }
        anim.SetTrigger("Fly");
    }

    void Die()
    {
        // Fire event
        if (PlayerDied != null)
        {
            PlayerDied();
        }
        anim.SetTrigger("Die");
    }



    void OnTriggerEnter(Collider other)
    {
        if (canCollide == true)
        {
            if (other.tag == "Finish")
            {
                if (SceneManager.GetActiveScene().name == "Level 1")
                {
                    PlayerPrefs.SetInt("Level 2", 1);
                }
                else if (SceneManager.GetActiveScene().name == "Level 2")
                {
                    PlayerPrefs.SetInt("Level 3", 1);
                }

                CoinScore.LevelFinish();
                Debug.Log("Lvl finished");

                if (SceneManager.GetActiveScene().name == "Level " + lastLevel)
                {
                    SceneManager.LoadScene("Credit");
                }
                else
                {
                    SceneManager.LoadScene("Lv Finish");
                }
            }
            if (other.tag == "Gold") //Hit gold
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.item);
                CoinManager.Instance.AddCoins(1);
                CreateParticle(goldParticlePrefab, other.transform.position);
                Destroy(other.gameObject);

                CoinScore.GetCoins();

            }
            else if (other.tag == "Boost")
            {

                CreateParticle(boostParticlePrefab, other.transform.position);
                Destroy(other.gameObject);
                Debug.Log("Boost");
                jumpForce += 500;
                boostTimer = 0;
                canCollide = false;
                boostingParticle.Play();
            }
            else if (other.tag == "Stamina")
            {
                Destroy(other.gameObject);
                stamina = maxStamina;
                CreateParticle(staminaParticlePrefab, other.transform.position);
                SoundManager.Instance.PlaySound(SoundManager.Instance.stamina);
            }
            else
            {
                // Hit obstacles
                if (!hitObstacle)
                    cameraController.ShakeCamera();
            }

         

            if (other.tag == "NormalObstacle") //hit obstacle
            {
                if (!hitObstacle)
                {
                    hitObstacle = true;

                    Die();
                    //rigid.velocity = new Vector3(0, 0, 0);
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0);

                    //Create particle base on obstacle
                    rigid.isKinematic = true;
                    StartCoroutine(WaitToDisableKinematic());

                    SoundManager.Instance.PlaySound(SoundManager.Instance.hit);
                }

            }
            else if (other.tag == "FireObstacle")
            {
                if (!hitObstacle)
                {
                    hitObstacle = true;

                    Die();
                    //rigid.velocity = new Vector3(0, 0, 0);
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0);

                    //Create particle base on obstacle
                    rigid.isKinematic = true;
                    CreateParticle(gameManager.fireParticle, transform.position);
                    player.transform.Find("Main").GetComponent<Renderer>().material.SetColor("_Color", gray);
                    player.transform.Find("LeftWing").GetComponent<Renderer>().material.SetColor("_Color", gray);
                    player.transform.Find("RightWing").GetComponent<Renderer>().material.SetColor("_Color", gray);
                    StartCoroutine(WaitToDisableKinematic());

                    SoundManager.Instance.PlaySound(SoundManager.Instance.hit);
                }
            }
            else if (other.tag == "IceObstacle")
            {
                if (!hitObstacle)
                {
                    hitObstacle = true;

                    Die();
                    //rigid.velocity = new Vector3(0, 0, 0);
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0);

                    //Create particle base on obstacle
                    rigid.isKinematic = true;
                    GameObject icePrefab = Instantiate(gameManager.iceParticle, player.transform.position, Quaternion.identity) as GameObject;
                    icePrefab.transform.parent = player.transform;
                    icePrefab.transform.localPosition += new Vector3(-0.05f, 0, -0.2f);
                    icePrefab.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    StartCoroutine(WaitToDisableKinematic());

                    SoundManager.Instance.PlaySound(SoundManager.Instance.hit);
                }
            }
            else if (other.tag == "ElectricObstacle")
            {
                if (!hitObstacle)
                {
                    hitObstacle = true;

                    Die();
                    // rigid.velocity = new Vector3(0, 0, 0);  //stop player
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0);

                    //Create particle base on obstacle
                    rigid.isKinematic = true;
                    CreateParticle(gameManager.electricParticle, transform.position, true);
                    StartCoroutine(WaitToDisableKinematic());

                    SoundManager.Instance.PlaySound(SoundManager.Instance.hit);
                }
            }
        }
        else
        {
            return;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (GameManager.Instance.GameState == GameState.GameOver && col.collider.tag.Equals("TheGround") && !hasHitGround)
        {
            hasHitGround = true;
            SoundManager.Instance.PlaySound(SoundManager.Instance.hit);
            CreateParticle(hitGroundParticlePrefab, transform.position + new Vector3(0, -1f, 0));
        }
    }

    void CreateParticle(ParticleSystem particle, Vector3 position, bool attachToParent = false, float existTime = 5f)
    {
        ParticleSystem particleTemp = Instantiate(particle, position, particle.transform.rotation) as ParticleSystem;
        if (attachToParent)
        {
            particleTemp.transform.SetParent(player.transform);
        }
        particleTemp.Play();
        Destroy(particleTemp.gameObject, existTime);
    }

    IEnumerator RotateParentPlayer()
    {
        turn = turn * (-1);

        float firstCurrentAngle = transform.eulerAngles.y; //Y rotation = 0
        while (firstCurrentAngle < rotateAngle && GameManager.Instance.GameState == GameState.Playing) //Rotate 
        {
            float rotateAmount = 200 * Time.deltaTime;
            firstCurrentAngle += rotateAmount;
            if (turn < 0)
            {
                transform.Rotate(Vector3.up * rotateAmount);
            }
            else
            {
                transform.Rotate(Vector3.down * rotateAmount);
            }
            yield return null;
        }
        if (turn < 0)
        {
            transform.eulerAngles = new Vector3(0, rotateAngle, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -rotateAngle, 0);
        }


        float secondCurrentAngle = transform.eulerAngles.y; //Y rotation = rotateAngle
        if (turn < 0)
        {
            while (secondCurrentAngle > 0 && GameManager.Instance.GameState == GameState.Playing)
            {
                float rotateAmount = 200 * Time.deltaTime;
                secondCurrentAngle -= rotateAmount;
                transform.Rotate(Vector3.down * rotateAmount);
                yield return null;
            }
        }
        else
        {
            while (secondCurrentAngle < 360 && GameManager.Instance.GameState == GameState.Playing)
            {
                float rotateAmount = 200 * Time.deltaTime;
                secondCurrentAngle += rotateAmount;
                transform.Rotate(Vector3.up * rotateAmount);
                yield return null;
            }
        }

        transform.eulerAngles = new Vector3(0, 0, 0);

        isFinishRotate = false;
    }

    IEnumerator WaitToDisableKinematic()
    {
        yield return 
      //  new WaitForSeconds(0.5f);
        rigid.isKinematic = false;
    }

}
