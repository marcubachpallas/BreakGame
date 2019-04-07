using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour {

    public float initSpeed = 0, speed;
    public GameObject bulletPrefab;

    public int bullets = 25;

    public TextMeshProUGUI bulletsLeft;

    public GameObject[] roomsPrefab;

    int roomCount = 2;
    // Use this for initialization
    void Start () {
            bulletsLeft.text = "Bullets left: " + bullets;
    }

    // Update is called once per frame
    void Update () {
        speed = initSpeed + ((float)roomCount / 50);
        transform.Translate(transform.forward * speed);
        Debug.Log(speed);

        if (bullets <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);      
        }

        if (Input.GetMouseButtonDown(0))
        {
            bullets--;
            bulletsLeft.text = "Bullets left: " + bullets;
            //Debug.Log("fire");
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = this.gameObject.transform.position;
            //bullet.transform.localScale = Vector3.one;
            bullet.transform.rotation = transform.rotation;

            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 force = ray.direction * 10000;
            //Debug.Log(force);
            Debug.DrawRay(ray.origin, ray.direction*10, Color.red,5,false);
            bullet.GetComponent<Rigidbody>().AddForce(force);

        }
    }

    public void UpdateBullets(int i)
    {
        bullets += i;
        bulletsLeft.text = "Bullets left: " + bullets;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name.ToString());

        if (other.gameObject.GetComponent<Piece_Info>())
        {
            if (!other.gameObject.GetComponent<Piece_Info>().hitted)
            {
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
}
