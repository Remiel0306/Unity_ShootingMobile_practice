using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace remiel
{
    public class SystemControl : MonoBehaviour
    {
        [SerializeField, Header("�����n��")] private Joystick joystick;
        [SerializeField, Header("���ʳt��"), Range(0, 300)] private float speed = 3.5f;
        [SerializeField, Header("�����V�ϥ�")] private Transform traDirectionIcon;
        [SerializeField, Header("�����V�ϥܽd��"), Range(0, 100)] private float rangeOirectionIcon = 2.5f;
        [SerializeField, Header("�������t��"), Range(0, 100)] private float speedTurn = 1.5f;
        [SerializeField, Header("�ʵe�Ѽƨ���")] private string parameterWalk;

        private Animator animator;
        private Rigidbody rigidbody;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            LookDriectionIconPos();
            UpdateAnimatoin();
            UpdateDirectionPos();
        }

        private void FixedUpdate()
        {
            Move();
            GetJoystickValue();
        }

        private void GetJoystickValue()
        {
            print("<color=yellow>����:" + joystick.Horizontal + "</color>");
        }

        private void Move()
        {
            rigidbody.velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed;
        }

        private void UpdateDirectionPos()
        {
            Vector3 pos = transform.position + new Vector3(joystick.Horizontal, 0, joystick.Vertical)*rangeOirectionIcon;
            traDirectionIcon.position = pos;
        }

        private void LookDriectionIconPos()
        {
            Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        //private void UpdateDirectionPos()
        //{
        //    Vector3 pos = transform.position + new Vector3(joystick.Horizontal, 0 ,joystick.Vertical) * rangeo
        //}

        private void UpdateAnimatoin()
        {
            bool run = joystick.Horizontal != 0 || joystick.Vertical != 0;
            animator.SetBool(parameterWalk, run);
        }

    }
}
