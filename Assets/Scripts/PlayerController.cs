using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    float timer = 0;
    public float initSpeed = 0, speed;
    public GameObject bulletPrefab;

    public int bullets = 25;

    public TextMeshProUGUI bulletsLeft, timeText, bestTime;

    public GameObject[] roomsPrefab;

    int roomCount = 3;

    public AudioClip[] fxStone, fxDmg;
    public AudioSource musicAudio;

    bool ended = false;
    public GameObject playerName, button, gameover;
    // Use this for initialization
    void Start () {

        StartCoroutine(FadeInAudioSource(musicAudio));

        bulletsLeft.text = "Bullets left: " + bullets;
        timeText.text = "Time Alive: " + (Mathf.Round(timer * 100f)/100f);
        float besttime = PlayerPrefs.GetFloat("Value", -1);
        bestTime.text = "Best Time: " + (Mathf.Round(besttime * 100f) / 100f); ;
        if (besttime == -1)
        {
            bestTime.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update () {
        if (bullets <= 0)
        {
            if(!ended)
                EndGame();
            else
            {
                return;
            }
        }
        

        timer += Time.deltaTime;
        timeText.text = "Time Alive: " + (Mathf.Round(timer * 100f) / 100f);

        speed = initSpeed + ((float)roomCount / 25);
        transform.Translate(transform.forward * speed);
        //Debug.Log(speed);


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if(touch.phase == TouchPhase.Began)
            {
                bullets--;
                bulletsLeft.text = "Bullets left: " + bullets;
                //Debug.Log("fire");
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = this.gameObject.transform.position;
                //bullet.transform.localScale = Vector3.one;
                bullet.transform.rotation = transform.rotation;

                //Rigidbody rb = bullet.GetComponent<Rigidbody>();

                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                Vector3 force = ray.direction * 10000;
                //Debug.Log(force);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5, false);
                bullet.GetComponent<Rigidbody>().AddForce(force);
            }
        }
        /*if (Input.GetMouseButtonDown(0))
        {
            bullets--;
            bulletsLeft.text = "Bullets left: " + bullets;
            //Debug.Log("fire");
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = this.gameObject.transform.position;
            //bullet.transform.localScale = Vector3.one;
            bullet.transform.rotation = transform.rotation;

            //Rigidbody rb = bullet.GetComponent<Rigidbody>();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 force = ray.direction * 10000;
            //Debug.Log(force);
            Debug.DrawRay(ray.origin, ray.direction*10, Color.red,5,false);
            bullet.GetComponent<Rigidbody>().AddForce(force);

        }*/
    }

    public void UpdateBullets(int i)
    {
        bullets += i;
        bulletsLeft.text = "Bullets left: " + bullets;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name.ToString());

        if (other.gameObject.GetComponent<Piece_Info>())
        {
            if (!other.gameObject.GetComponent<Piece_Info>().hitted)
            {
                PlayFXDmg();
                UpdateBullets(-10);
                bulletsLeft.GetComponentInParent<Canvas>().transform.GetChild(0).GetComponent<Animation>().Play();
            }
            other.transform.parent.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "RoomTrigger")
        {
            Destroy(other.gameObject);
            roomCount++;
            int random = Random.Range(0, roomsPrefab.Length);
            GameObject instance = Instantiate(roomsPrefab[random]);
            instance.transform.position = new Vector3(0, 0, 100 * roomCount);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString("High", playerName.GetComponent<TMP_InputField>().text);
        PlayerPrefs.SetFloat("Value", (Mathf.Round(timer * 100f) / 100f));
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().name, 2));
        playerName.SetActive(false);
        button.SetActive(false);
        gameover.transform.localPosition = new Vector3(0, 0, 25);
    }

    void EndGame()
    {
        ended = true;
        float value = PlayerPrefs.GetFloat("Value", -1);

        if (timer > value)
        {
            playerName.SetActive(true);
            button.SetActive(true);
            gameover.SetActive(true);
            //open canvas to name
        }
        else
        {
            gameover.SetActive(true);
            gameover.transform.localPosition = new Vector3(0, 0, 25);

            StartCoroutine(LoadScene(SceneManager.GetActiveScene().name, 2));
        }

    }

    IEnumerator LoadScene(string sceneName, float time)
    {
        StartCoroutine(FadeOut(musicAudio));
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);

    }

    public void PlayFXStone()
    {
        int random = Random.Range(0, fxStone.Length);
        AudioSource audio = this.gameObject.AddComponent<AudioSource>();
        audio.PlayOneShot(fxStone[random]);
        StartCoroutine(DestroyAudio(audio));
    }

    public void PlayFXDmg()
    {
        int random = Random.Range(0, fxDmg.Length);
        AudioSource audio = this.gameObject.AddComponent<AudioSource>();
        audio.PlayOneShot(fxDmg[random]);
        StartCoroutine(DestroyAudio(audio));
    }

    IEnumerator DestroyAudio(AudioSource audio)
    {
        while (audio.isPlaying)
        {
            yield return null;
        }
        Destroy(audio);
    }
    public IEnumerator FadeOut(AudioSource source)
    {

        yield return new WaitForSeconds(1);
        source.volume = 1f;

        while (source.volume > 0.001f)
        {
            source.volume -= 1 * Time.deltaTime / 1;
            yield return null;
        }
        source.volume = 0.0f;
        source.Stop();
        SceneManager.LoadScene("MainScene");

    }

    //Fade In the audiosource we choose
    public IEnumerator FadeInAudioSource(AudioSource source)
    {
        while (source.volume < 1.0f)
        {
            Debug.Log(source.volume);
            source.volume += 1 * Time.deltaTime / 1;
            yield return null;
        }
        source.volume = 1.0f;
        source.loop = true;
    }
}
