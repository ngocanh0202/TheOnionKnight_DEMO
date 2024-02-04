using System.Collections;
using TMPro;
using UnityEngine;

public class BowController : MonoBehaviour
{
    Vector2 position_bow;
    [SerializeField] GameObject gameObject_player;
    [SerializeField] float radius_bow;
    [SerializeField] Animator animator_bow;
    public bool isShotting;
    [SerializeField] float cooldownShotting;
    [SerializeField] GameObject gameObjects_arrow;
    [SerializeField] public int numbers_arrow;
    [SerializeField] TextMeshProUGUI mesh_arrow;
    // Start is called before the first frame update
    void Awake()
    {
       isShotting = false;
       mesh_arrow.text = numbers_arrow.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Number of arrow"+ numbers_arrow);
        if(isShotting) return;
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 dir = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        transform.up = dir;


        float distance = Vector2.Distance(gameObject_player.transform.position, transform.position);
        if(distance >  radius_bow){
            Vector2 directionToCenter = (gameObject_player.transform.position - transform.position).normalized;
            Vector2 center = new Vector2(gameObject_player.transform.position.x, gameObject_player.transform.position.y);
            transform.position = center - directionToCenter * radius_bow;
        }
    }
    public void Attacking(){
        if(!isShotting && numbers_arrow > 0){
            isShotting = true;
            animator_bow.SetBool("isShot", true);
            StartCoroutine(shotting());
        }
    }
    IEnumerator shotting(){
        yield return new WaitForSeconds(cooldownShotting);
        isShotting = false;
        animator_bow.SetBool("isShot", false);
        numbers_arrow--;
        mesh_arrow.text = numbers_arrow.ToString();
        Instantiate(gameObjects_arrow, transform.position, transform.rotation);
    }
}
