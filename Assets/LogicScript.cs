using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;
public class LogicScript : MonoBehaviour
{
    public Text HpTexT;
    public void UpdateplayerUI(float hp)
    {
        HpTexT.text = "Hp:" + math.round(hp).ToString();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
