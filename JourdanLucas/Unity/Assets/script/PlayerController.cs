using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 6f;
    public float TurnSpeed = 120f;
    public float PitchRange = 0.2f;
    public float speedRot = 4;
    public float timeToReaload = 1;

    public new Camera camera;
    public Transform reticleTransform;
    public GameObject partToRatate;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public float numberOfEnemies;
    public GameObject win;

    private string MovementAxisName;
    private string TurnAxisName;
    private Rigidbody Rigidbody;
    private float MovementInputValue;
    private float TurnInputValue;
    private bool condition;
    private float score = 0;
    Vector3 dir;
    Quaternion rotDir;

    public void AddScore()
    {
        score += score + 1;
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Rigidbody.isKinematic = false;
        MovementInputValue = 0f;
        TurnInputValue = 0f;
    }

    private void OnDisable()
    {
        Rigidbody.isKinematic = true;
    }

    private void Start() //start
    {
        win.SetActive(false);
        MovementAxisName = "Vertical";
        TurnAxisName = "Horizontal";
        condition = true;
        StartCoroutine(FireBrain());
    }

    private void Update() //juste Update ^^
    {
        MovementInputValue = Input.GetAxis(MovementAxisName);
        TurnInputValue = Input.GetAxis(TurnAxisName);

        Reticle();
        CheckIfAllEnemyIsDead();
    }

    private void FixedUpdate() //fixedUpdate
    {
        Move();
        Turn();
    }

    private void Move() // can move the tank
    {
        Vector3 movement = transform.forward * MovementInputValue * Speed * Time.deltaTime;
        Rigidbody.MovePosition(Rigidbody.position + movement);

    }

    private void Turn() //can turn the tank
    {
        float turn = TurnInputValue * TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);
    }

    private void Reticle() //update commande for the Reticle
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            reticleTransform.position = hit.point;

            dir = (reticleTransform.position - transform.position).normalized;
            rotDir = Quaternion.LookRotation(dir);
            partToRatate.transform.rotation = Quaternion.Slerp(partToRatate.transform.rotation, rotDir, speedRot /1000f);
        }
    }

    IEnumerator FireBrain() //if the player press fire (right ou left clic
    {
        do 
        {
            if (Input.GetButton("Fire") && condition == true)
            {
                condition = false;

                Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

                yield return new WaitForSeconds(timeToReaload);
                condition = true;
            }
            yield return new WaitForSeconds(0);

        } while (true);
    }

    public void CheckIfAllEnemyIsDead()
    {
        if(score > numberOfEnemies)
        {
            Debug.Log("Win");
            win.SetActive(true);
        }
    }
}