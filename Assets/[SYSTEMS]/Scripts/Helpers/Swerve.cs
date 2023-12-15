using UnityEngine;

namespace Helpers
{
    public class Swerve
    {
        [Header("Positions Of Mouse Gestures")]
        private static Vector3 firstMousePos, lastMousePos;

        [Header("Clamps")]
        [Range(0, 10)]
        [SerializeField] private static float clampOnAxis;
        [Range(0, 60)]
        [SerializeField] private static float rotationClamp;
        [Range(0, 20)]
        [SerializeField] private static float rotationDelay;

        [Range(0, .1f)]
        [SerializeField] private static float sensForMove;
        [Range(0, 100)]
        [SerializeField] private static float forwardSpeed;

        [Header("Delays")]
        [SerializeField] private static float delayOfZeroRotation;

        [Header("Booleans")]
        [SerializeField] private static bool checkForOnlyInput;
        [SerializeField] private static bool checkForBoolLevelParams;


        private static Vector3 direction;
        private static Vector3 movementDirection;


        private static Vector3 differVec;
        private static Vector3 rotVec;
        private static Vector3 simpleVector;
        /// <summary>
        /// Set object speed on direction of Vector3.forward
        /// </summary>
        public static void MoveOnLine(Transform Transform, float speed)
        {
            Transform.position += movementDirection * speed * Time.deltaTime; //Objeyi ilerlettiðimiz kýsým
        }
        /// <summary>
        /// Set object transform, clamp value on the X Axis, sensivity on the X Axis, checker for only Input
        /// </summary>
        /// <retur>
        public static void MoveAndRotateOnAxis(
            Transform Transform,
            float clampOnAxis,
            bool doRotation = false,
            bool checkForClamp = false,
            bool checkForOnlyInput = false,
            Enums.Axis axis = Enums.Axis.X,
            float sensForMove = .15f,
            float rotationClamp = 30,
            float rotationDelay = 5)
        {
#if !UNITY_EDITOR
            if (checkForOnlyInput) //Ekrana birden fazla parmak dokununca hata almamak adýna if kontrolü.
                if (Input.touchCount != 1)
                    return;
#endif
            if (Input.GetMouseButtonDown(0)) //Ýlk dokunduðumuz noktanýn pozisyonunu almak için if kontrolü
                firstMousePos = Input.mousePosition;

            if (Input.GetMouseButton(0)) //Ýlk dokunmadan sonra sürekli pozisyon bilgisi almak için if kontrolü
            {
                lastMousePos = Input.mousePosition; //Aldýðýmýz sürekli pozisyon deðeri (devamlý olarak dokunduðumuz pozisyonla kendini yeniliyor)

                differVec = lastMousePos - firstMousePos; //Ýlk ve sonraki input pozisyonlarýnýn farkýný alýyoruz

                simpleVector = new Vector3(differVec.x * direction.x, differVec.y * direction.y, differVec.z * direction.z) * .8f; //Switch-Case'den aldýðýmýz bilgiye göre hareket edeceðimiz pozisyonu belirliyoruz

                differVec = Transform.position + (simpleVector * Scripts.DataManager().data.horizontalSpeed * sensForMove * Time.deltaTime); //Çeþitli hassaslýk ayarlarý. -->Smooth'luðu saðlamak için

                Transform.position = differVec; //Hassaslýk ayarlarýndan sonra elde ettiðimiz veriyi Objemizin pozisyonuna eþitliyoruz.

                switch (axis) //Swerve yapacaðýmýz ekseni ve ilerleyeceðimiz ekseni belirlediðimiz kýsým
                {
                    case Enums.Axis.X:
                        movementDirection = Vector3.forward;
                        direction = Vector3.right;
                        rotVec = Vector3.up;
                        if (checkForClamp)
                            Transform.position = new Vector3(Mathf.Clamp(Transform.position.x, -clampOnAxis, clampOnAxis), Transform.position.y, Transform.position.z);
                        break;
                    case Enums.Axis.Y:
                        movementDirection = Vector3.right;
                        direction = Vector3.up;
                        rotVec = Vector3.forward;
                        if (checkForClamp)
                            Transform.position = new Vector3(Transform.position.x, Mathf.Clamp(Transform.position.y, -clampOnAxis, clampOnAxis), Transform.position.z);
                        break;
                    case Enums.Axis.Z:
                        movementDirection = Vector3.up;
                        direction = Vector3.right;
                        rotVec = Vector3.back;
                        if (checkForClamp)
                            Transform.position = new Vector3(Transform.position.x, Transform.position.y, Mathf.Clamp(Transform.position.z, -clampOnAxis, clampOnAxis));
                        break;
                }

                if (doRotation) //Obje swerve yaparken rotasyonunu da etkilensin istersek buradaki if kontrolüne giriyoruz
                {
                    if (simpleVector.x > 0 || simpleVector.y > 0 || simpleVector.z > 0)
                        Transform.rotation = Quaternion.Slerp(Transform.rotation, Quaternion.Euler(rotVec * rotationClamp), rotationDelay * Time.deltaTime);
                    else if (simpleVector.x < 0 || simpleVector.y < 0 || simpleVector.z < 0)
                        Transform.rotation = Quaternion.Slerp(Transform.rotation, Quaternion.Euler(rotVec * -rotationClamp), rotationDelay * Time.deltaTime);
                    else
                        Transform.rotation = Quaternion.Slerp(Transform.rotation, Quaternion.Euler(0, 0, 0), rotationDelay * Time.deltaTime);
                }

                firstMousePos = Input.mousePosition; //Ýlk ve son inputtaki pozisyon farklýlýðýný yeniliyoruz ki parmaðýmýzý soldan saða getirip sabit tutunca hala elimizi çekmezsek hareket etmeyelim. (Joystick gibi davranmamak için)
            }
            else if (doRotation)
                Transform.rotation = Quaternion.Slerp(Transform.rotation, Quaternion.Euler(0, 0, 0), rotationDelay * Time.deltaTime); // Ýnput olmadýðýnda rotasyonumuzu sýfýrlýyoruz
        }


        public static void MoveOnLine(Rigidbody rigidbody, float speed)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, speed * Time.fixedDeltaTime);
        }


        /// <summary>
        /// Set object transform, clamp value on the X Axis, sensivity on the X Axis, checker for only Input
        /// </summary>
        /// <retur>
        public static void MoveAndRotateOnAxis(
            float _vSpeed,
            float _hSpeed,
            Rigidbody playerRb,
            bool _isDown, bool _isMouse,
            bool checkForOnlyInput = true)
        {

#if !UNITY_EDITOR
            if (checkForOnlyInput) //Ekrana birden fazla parmak dokununca hata almamak adýna if kontrolü.
                if (Input.touchCount != 1)
                    return;
#endif
            if (_isDown) //Ýlk dokunduðumuz noktanýn pozisyonunu almak için if kontrolü
                firstMousePos = Input.mousePosition;

            if (_isMouse) //Ýlk dokunmadan sonra sürekli pozisyon bilgisi almak için if kontrolü
            {
                lastMousePos = Input.mousePosition; //Aldýðýmýz sürekli pozisyon deðeri (devamlý olarak dokunduðumuz pozisyonla kendini yeniliyor)

                differVec = lastMousePos - firstMousePos; //Ýlk ve sonraki input pozisyonlarýnýn farkýný alýyoruz

                simpleVector = new Vector3(differVec.x, 0, 0) * .8f; //Switch-Case'den aldýðýmýz bilgiye göre hareket edeceðimiz pozisyonu belirliyoruz
                if (firstMousePos == Vector3.zero || differVec.x > 50)
                    _hSpeed = 0;
                playerRb.velocity = new Vector3(simpleVector.x * _hSpeed * Time.fixedDeltaTime, playerRb.velocity.y, playerRb.velocity.z);
                //Debug.Log($"{firstMousePos}  {lastMousePos}  {differVec}  {simpleVector}  {playerRb.velocity.x}");
                firstMousePos = Input.mousePosition; //Ýlk ve son inputtaki pozisyon farklýlýðýný yeniliyoruz ki parmaðýmýzý soldan saða getirip sabit tutunca hala elimizi çekmezsek hareket etmeyelim. (Joystick gibi davranmamak için)
            }
            else
                playerRb.velocity = new Vector3(0, playerRb.velocity.y, _vSpeed * Time.fixedDeltaTime);
            
        }

    public static void MoveAndRotateOnAxis(
    float _vSpeed,
    float _hSpeed,
    CharacterController charController,
    bool checkForOnlyInput = true)
        {
#if !UNITY_EDITOR
            if (checkForOnlyInput) //Ekrana birden fazla parmak dokununca hata almamak adýna if kontrolü.
                if (Input.touchCount != 1)
                    return;
#endif
            if (Input.GetMouseButtonDown(0)) //Ýlk dokunduðumuz noktanýn pozisyonunu almak için if kontrolü
                firstMousePos = Input.mousePosition;

            if (Input.GetMouseButton(0)) //Ýlk dokunmadan sonra sürekli pozisyon bilgisi almak için if kontrolü
            {
                lastMousePos = Input.mousePosition; //Aldýðýmýz sürekli pozisyon deðeri (devamlý olarak dokunduðumuz pozisyonla kendini yeniliyor)

                differVec = lastMousePos - firstMousePos; //Ýlk ve sonraki input pozisyonlarýnýn farkýný alýyoruz

                simpleVector = new Vector3(differVec.x, 0, 0) * .8f; //Switch-Case'den aldýðýmýz bilgiye göre hareket edeceðimiz pozisyonu belirliyoruz
                charController.Move(new Vector3(simpleVector.x * _hSpeed * Time.fixedDeltaTime, charController.velocity.y, charController.velocity.z));

                firstMousePos = Input.mousePosition; //Ýlk ve son inputtaki pozisyon farklýlýðýný yeniliyoruz ki parmaðýmýzý soldan saða getirip sabit tutunca hala elimizi çekmezsek hareket etmeyelim. (Joystick gibi davranmamak için)
            }
            else if(firstMousePos != Vector3.zero)
                charController.Move(new Vector3(0, charController.velocity.y, _vSpeed * Time.fixedDeltaTime));
        }
    }
}
