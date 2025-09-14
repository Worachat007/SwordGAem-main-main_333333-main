
นาย วรชาติ ปิตตานัง 673450039-8




อนิเมชันและการควบคุมตัวละคร
อนิเมชันการยืน (Idle)

<img width="954" height="980" alt="image" src="https://github.com/user-attachments/assets/90d5a6d3-7968-49e9-8468-441a53ee0c52" />


เมื่อสร้างอนิเมชัน Player_idle ให้กำหนดเป็นสีส้ม ซึ่งหมายความว่ามันคือ ค่าเริ่มต้น (Default State) ของ Animator เมื่อผู้เล่นไม่ได้กดปุ่มใด ๆ ตัวละครจะอยู่กับที่และเล่นอนิเมชัน Idle โดยอัตโนมัติ

อนิเมชันการเคลื่อนไหว (เดินหน้า/ถอยหลัง)

<img width="907" height="162" alt="image" src="https://github.com/user-attachments/assets/377bbc28-8c07-4172-bec4-2bc3ae059516" />

สร้างอนิเมชันการเดินและเชื่อมโยงกับ Player_idle โดยทำ Transition ไป-กลับ

เมื่อมีการเคลื่อนที่ → เปลี่ยนจาก Player_idle ไปยัง Move

เมื่อหยุดนิ่ง → กลับจาก Move มายัง Player_idle ซึ่งเป็นสถานะเริ่มต้น

การตั้งค่าการควบคุมการเคลื่อนที่

<img width="909" height="391" alt="image" src="https://github.com/user-attachments/assets/7e7ee67e-9aca-4c0a-88d3-991add1dffcc" />


เปิด Input System แล้วเพิ่ม Action ใหม่:

กดปุ่ม + ที่ Actions แล้วตั้งชื่อ Action ว่า Player จากนั้นจะมีคอลัมน์ Actions โผล่ขึ้นมา

เพิ่ม Action ย่อยชื่อ Jump → Binding กับปุ่ม Space หรือปุ่ม ลูกศรขึ้น

สร้าง Action ใหม่ชื่อ Move

กำหนด Action Type เป็น Value

ตั้งค่า Control Type เป็น Any

เพิ่ม Composite Binding (1D Axis) → ตั้งชื่อว่า LeftRight

Negative = ปุ่ม A

Positive = ปุ่ม D

เขียนสคริปต์ควบคุมการเดิน
GatherInput.cs

public class GatherInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    private Control myControl;
    public float valueX;
    public bool JumpInput;

    public void Awake()
    {
        myControl = new Control();
    }
    private void OnEnable()
    {
        myControl.Player.Move.performed += StartMove;
        myControl.Player.Move.canceled += StopMove;
        myControl.Player.Jump.performed += JumpStart;
        myControl.Player.Jump.canceled += JumpStope;
        myControl.Player.Enable();
    }
    public void OnDisable()
    {
        myControl.Player.Move.performed -= StartMove;
        myControl.Player.Move.canceled -= StopMove;
        myControl.Player.Jump.performed -= JumpStart;
        myControl.Player.Jump.canceled -= JumpStope;
        myControl.Player.Disable();
        valueX = 0; 
        //myControl.Disable();
    }
    private void StartMove(InputAction.CallbackContext ctx) {
        valueX = ctx.ReadValue<float>();
    }
    private void StopMove(InputAction.CallbackContext ctx) {
        valueX = 0;
    }

ไฟล์นี้ทำหน้าที่รับ Input ที่ผู้เล่นกด แล้วส่งต่อข้อมูลไปยังระบบการเคลื่อนไหว โดยใช้ Event เพื่อตรวจจับการเริ่ม/หยุดเคลื่อนที่ และคำสั่งกระโดด

PlayerMoveControls.cs

public class PlayerMoveControls : MonoBehaviour
{
    public float speed = 5f;
    private int direction = 1;
    public float jumpForce = 5;
    private bool grounded = false;

    private GatherInput gatherInput;
    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
        SetAnimatorValues();
        Flip();
        Rigidbody2D.linearVelocity = new Vector2(speed * gatherInput.valueX,
            Rigidbody2D.linearVelocity.y);
        JumpPlayer();
    }
    public Transform leftPoint;
    public float rayLength;
    public LayerMask groundLayer;
    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position,
            Vector2.down, rayLength, groundLayer);
       
        grounded = leftCheckHit;
        // print(grounded);
    }
    private void JumpPlayer() {
        if (gatherInput.JumpInput) {
            Rigidbody2D.linearVelocity = new Vector2(gatherInput.valueX * speed, jumpForce);
        }
        gatherInput.JumpInput = false;
    }
    private void SetAnimatorValues()
    {
        animator.SetFloat("speed", Mathf.Abs(Rigidbody2D.linearVelocityX));
        animator.SetFloat("Vspeed", Rigidbody2D.linearVelocityY);
        animator.SetBool("Ground", grounded);
    }
    /// <summary>
    /// for flip character
    /// </summary>
    private void Flip() {
        if (gatherInput.valueX * direction < 0) {
            //to flip character
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            //update direction
            direction *= -1;
        }
    }
}

ไฟล์นี้ใช้สำหรับแปลงค่าที่ได้จาก GatherInput.cs มาเป็นการเคลื่อนไหวจริง ๆ ของตัวละคร เช่น การเดินไปซ้าย ขวา หรือการกระโดด

มีตัวแปร direction เพื่อกำหนดทิศทางของ Sprite

ค่า บวก = ตัวละครหันขวา (ทิศทางปกติ)

ค่า ลบ = ตัวละครหันซ้าย (โดยการกลับด้าน Scale ของแกน X)

สร้างฟังก์ชัน Flip() เพื่อตรวจสอบทิศทางและสลับด้านของตัวละครโดยแก้ค่า localScale.x ให้เป็นค่าติดลบ

ฟังก์ชันนี้จะถูกเรียกใช้ภายใน Update() ซึ่งจะทำงานทุกเฟรม ทำให้ทิศทางของตัวละครอัปเดตแบบเรียลไทม์

ระบบการกระโดด
ฟังก์ชันการกระโดดใน GatherInput.cs


		//...
    public bool jumpInput;
    
    private void OnEnable()
    {
		    //...
        myControl.Player.Jump.performed += JumpStart;
        myControl.Player.Jump.canceled += JumpStop;

        myControl.Player.Enable();
    } 

    private void OnDisable()
    {
		    //...
        myControl.Player.Jump.performed -= JumpStart;
        myControl.Player.Jump.canceled -= JumpStop;

        myControl.Player.Disable();
    }
   
    private void JumpStart(InputAction.CallbackContext ctx)
    {
        jumpInput = true;
    }
    private void JumpStop(InputAction.CallbackContext ctx)
    {
        jumpInput = false;
    }
}

เมื่อผู้เล่นกดปุ่มกระโดด ระบบจะเปลี่ยนสถานะของตัวละคร และส่งค่าการกระโดดไปยัง PlayerMoveControls

เพิ่มการเคลื่อนไหวแนวดิ่งใน PlayerMoveControls.cs

public class PlayerMoveControls : MonoBehaviour
{
		//...
    public float jumpForce;
    //...
    private void FixedUpdate()
    {
        Move();
        JumpPlayer();
    }
		//...
    private void JumpPlayer() 
    {
        if (gatherInput.jumpInput)
        {
            rigidbody2D.velocity = new Vector2(gatherInput.valueX * speed, jumpForce);
        }
        gatherInput.jumpInput = false;
    }
}

ทำให้ตัวละครเคลื่อนที่ขึ้นด้านบนตามแกน Y เพื่อให้ดูเหมือนการกระโดดจริง

อนิเมชันการกระโดด

<img width="1719" height="422" alt="image" src="https://github.com/user-attachments/assets/ff518e09-40ed-4217-bee6-ee7497e6b3f6" />


ดับเบิ้ลคลิกที่ Blend Tree ของ Animator จะเห็นว่ามี Blend Type = 1D (มิติเดียว)

เดิมเรามีตัวแปร Speed

เพิ่มตัวแปรใหม่ชื่อ vSpeed (float) เพื่อตรวจจับความเร็วแนวดิ่ง

จากนั้นปรับ Blend Tree ให้ใช้ vSpeed เป็น Parameter และเพิ่ม Motion 3 ค่า:

Jump Down

Jump Peak

Jump Up

ระบบความเสียหายและการตาย

<img width="341" height="297" alt="image" src="https://github.com/user-attachments/assets/23234ba7-0967-43a8-854c-214e7c110b00" />


PlayerStats.cs

สร้าง Empty Object ชื่อ PlayerStats ไว้ในตัวละคร และเพิ่ม Polygon Collider 2D ที่เปิด Is Trigger เพื่อใช้ตรวจสอบการโดนโจมตี (ไม่เกี่ยวกับการยืนบนพื้น) จากนั้นสร้าง Layer PlayerStats

เขียนสคริปต์ PlayerStats.cs:

using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    private bool canTakeDamage = true;

    void Start()
    {
        health = maxHealth; // เริ่มเกมมาด้วยเลือดเต็ม
    }

    public void TakeDamage(float damage)
    {
        if(!canTakeDamage) return; // ถ้ากำลังอยู่ในช่วงกันดาเมจ จะไม่รับความเสียหายเพิ่ม

        health -= damage;
        if(health <= 0)
        {
            // เมื่อตาย ปิด Collider และหยุดควบคุมการเคลื่อนไหว
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponentInParent<GatherInput>().DisableControls();
            Debug.Log("Player is dead");
        }

        StartCoroutine(DamagePrevention());
    }

    private IEnumerator DamagePrevention()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.15f); // กันดาเมจชั่วคราว 0.15 วิ
        if (health > 0) canTakeDamage = true;
    }
}


📌 สรุป

maxHealth = ค่าพลังชีวิตสูงสุด

health = ค่าพลังชีวิตปัจจุบัน

TakeDamage() = หักพลังชีวิต และถ้าเลือดหมด → ปิดการควบคุมตัวละคร

ใช้ Coroutine เพื่อเว้นระยะรับดาเมจ ไม่ให้ถูกหักเลือดรัว ๆ ภายในเสี้ยววินาที

การหยุดควบคุมเมื่อ Player ตาย

เพิ่มฟังก์ชันใน GatherInput.cs:

public void DisableControls() 
{
    myControl.Player.Move.performed -= StartMove;
    myControl.Player.Move.canceled -= StopMove;

    myControl.Player.Jump.performed -= JumpStart;
    myControl.Player.Jump.canceled -= JumpStop;

    myControl.Player.Disable();
    valueX = 0;
}

การสร้าง Spike (วัตถุที่ทำดาเมจ)

เพิ่ม Sprite (เช่น Sword) ลงในฉาก → เปลี่ยนชื่อเป็น Spike

ใส่ Rigidbody2D และตั้งค่า Body Type = Kinematic

เพิ่ม Polygon Collider 2D และเปิด Is Trigger

สร้าง Layer EnemyAttack แล้วกำหนดให้ Spike อยู่ในเลเยอร์นี้

ไปที่ Project Settings → Physics 2D → ให้ PlayerStats สามารถชนกับ EnemyAttack

Spikes.cs
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerStats"))
        {
            Debug.Log("Player hit by spikes");
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}


📌 สรุป

damage = ค่า Damage ที่ Spike สร้างได้

ใช้ OnTriggerEnter2D() ตรวจจับการชนกับ PlayerStats

ถ้า Player ชน Spike → เรียก TakeDamage() เพื่อลดพลังชีวิต

ภาพรวมการทำงาน

ตัวละครมีระบบเลือด (PlayerStats)

เมื่อชน Spike → เลือดลดลง

ถ้าเลือดหมด → ตัวละครหยุดเคลื่อนไหวและขึ้นข้อความว่า Player is dead

ใช้ Coroutine กันไม่ให้โดน Damage ซ้ำรัว ๆ

สามารถสังเกตการทำงานได้จาก Console
