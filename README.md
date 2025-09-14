# Survival_catparty_3-main
การยืน
<img width="1133" height="1029" alt="image" src="https://github.com/user-attachments/assets/38a4015e-9d28-4291-82c0-d510c8cb3d98" />

# เพิ่ม Collider

เลือกที่ตัวละครของเรา ให้เราทำการใส่ Rigidbody 2D และเพิ่ม Collider 2D เข้าไปให้กับวัตถุ โดย Collider จะใช้เป็น Capsule Collider 2D เพื่อแก้ปัญหาการชนกันของ Collider กรณีที่มีมุมของ Pixel ที่มองไม่เห็น และให้ปรับขนาดของ Collider ตัวละครของเรา

ที่ Rigibody 2d ให้ทำการปรับ Constraints โดยกำหนด Freeze Rotation

สร้างขึ้นทั้ง `Ground` และ `Player`

# Input System

ที่ Unity เปิด Package Manager และทำการติดตั้ง Input System 

สร้างไฟล์ใหม่ ที่ Assets โดยทำการคลิ๊กขวา เลือก Create→ Input Actions ตั้งชื่อว่า Controls

กดเปิดไฟล์ทีสร้างขึ้น โปรแกรมจะเปิดหน้าต่างใหม่ให้เรา ลักษณะเป็นสามคอลัมน์

- Action Maps สำหรับกำหนดว่า อะไรจะใช้ Controles
- Action
- Properties

ที่ Action สร้าง Action ใหม่ โดยกดปุ่ม + แล้วตั้งชื่อว่า Player จะปรากฏ Actions ใหม่ขึ้นมาที่หน้าคอลัมน์ Actions ตั้งชื่อว่า Jump

ที่ด้านล่างของ Actions กดเลือกเพื่อทำการ Binding หรือการผูกคำสั่งนี้กับปุ่มที่เราต้องการ กดเลือก  Binding Path ให้เป็น Space และ ปุ่มเครื่องหมาย ขึ้น

สร้าง Actions ใหม่ชื่อ Move กำหนด Action Type เป็น Value และ Control Type เป็น Any

- ทำการเพิ่ม Negative/Positive Binding ให้กับ  Move ตั้งชื่อว่า LeftRight
- ที่ด้านล่างของ Move จะมี Negative และ Positive ทำการ Binding คีย์ของปุ่ม โดย Negative เป็นปุ่ม `A` และ Positive เป็นปุ่ม `D`

บันทึกการตั้งค่าที่ด้านบน `Save Asset`
อนิเมะชั่นเดินหน้าถอยหลัง
<img width="735" height="114" alt="image" src="https://github.com/user-attachments/assets/814fc93d-239d-4503-995c-b6304fca9cc3" />




การเดินหน้ากลับหลัง
<img width="920" height="363" alt="image" src="https://github.com/user-attachments/assets/c2f8fed9-71b9-4c39-82d5-04534f78373b" />






การกระโดด
<img width="1726" height="475" alt="image" src="https://github.com/user-attachments/assets/d26c6675-ead3-47ae-af9d-698dcd3b28fb" />


บาทเจ็บ
<img width="198" height="75" alt="image" src="https://github.com/user-attachments/assets/158640a1-df61-459c-bfe7-e306dacfa305" />

หนาม

<img width="702" height="699" alt="image" src="https://github.com/user-attachments/assets/53c1d646-de40-4173-a5ca-526c88908b94" />




ตาย
<img width="230" height="129" alt="image" src="https://github.com/user-attachments/assets/30da6975-55f8-430b-b93b-52999d4b2fc4" />
