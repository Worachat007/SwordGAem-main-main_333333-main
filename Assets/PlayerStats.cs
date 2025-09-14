using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerStats : MonoBehaviour
{
    public float MaxHealth;
    public float Health;
    private bool CanTakeDamage = true;
    private Animator animator;
    public LogicScript logicScript;

    public GameObject over;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = MaxHealth;
        animator = GetComponentInParent<Animator>();
        logicScript = Object.FindFirstObjectByType<LogicScript>();
        logicScript.UpdateplayerUI(Health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float Damage)
    {
        if (!CanTakeDamage)
            return;     
        Health -= Damage;
        animator.SetBool("Damage", true);
        logicScript.UpdateplayerUI(Health);
        if (Health <= 0)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponentInParent<GatherInput>().OnDisable();
            Debug.Log("Player is Dead");
            over.SetActive(true);
        }
        StartCoroutine(DamagePrevention());
    }
    public void Restar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator DamagePrevention() 
    {
        CanTakeDamage = false;
        yield return new WaitForSeconds(0.15f);
        if (Health > 0)
        {
            CanTakeDamage = true;
            animator.SetBool("Damage", false);
        }
        else
        {
            animator.SetBool("Death", true);
        }

    }
}
