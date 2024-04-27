using System.Collections.Generic;
using UnityEngine;

public class Observe : MonoBehaviour
{
    public float RotateSpeed;
    public float MoveSpeed;
    public const float FreeMaxPitch = 80;
    public enum CameraStatus {freeCamera=0,player};
    public CameraStatus _cameraStatus;
    public Player _target;//Ŀ������
    private List<Player> _players;
    public UnityEngine.Transform initialTransform;
    private int _playerNumber;
    Vector3 offset;//��������ƫ����
    public float rotationSpeed;//�������ת�ٶ�
    void Start()
    {
        offset = new Vector3(5, 5, 5);
        initialTransform = transform;
        _players = new();
        RotateSpeed = 100f;
        rotationSpeed = 75f;
        MoveSpeed = 10f;
        _cameraStatus = CameraStatus.freeCamera;
        _target = null;
        //��֤���������Ŀ�����壬��z����ת����0
        // transform.position = _target.playerObj.transform.position - offset;
        //transform.LookAt(_target.playerObj.transform.position);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        //�õ������������֮��ĳ�ʼƫ����

    }

    void Update()
    {
        if (_cameraStatus == CameraStatus.player)
        {
            Rotate();
            Rollup();
            ExchangeStatus();
            Follow();
        }
        else
        {
            Move();
            ExchangeStatus();
        }

    }
    void visualAngleReset()
    {
        offset = new Vector3(-5, 5, -5);
        transform.position = _target.playerObj.transform.position + offset;
        transform.LookAt(new Vector3(_target.playerObj.transform.position.x, _target.playerObj.transform.position.y+1.5f, _target.playerObj.transform.position.z));
        //transform.eulerAngles = new Vector3(initialTransform.eulerAngles.x, initialTransform.eulerAngles.y, 0);
    }
    //��������桢�������Ź���:
    void ExchangeStatus()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Dictionary<int ,Player> dict= PlayerSource.GetPlayers();
            _players.Clear();
            foreach (KeyValuePair<int, Player> player in dict)
            {
                _players.Add(player.Value);
            }
            if(_cameraStatus == CameraStatus.player)
            {
                // Retry target
                if (_players.Count-1 >= _playerNumber)
                {
                    _target = _players[_playerNumber];
                    Debug.Log(transform.position);
                    Debug.Log($"target {_target.playerObj.transform.position}");
                    visualAngleReset();
                    Debug.Log($"after {transform.position}");
                    _playerNumber += 1;
                }
                else
                {
                    _cameraStatus = CameraStatus.freeCamera;
                    _playerNumber = 0;
                }

            }
            else if(_cameraStatus == CameraStatus.freeCamera && _players.Count !=0)
            {
                _cameraStatus = CameraStatus.player;
                _target = _players[_playerNumber];
                visualAngleReset();
                _playerNumber += 1;
            }
        }
    }
    public float zoomSpeed = 1f; // ��Ұ�������ٶ�
    float zoom;//���ֹ�����
    void Follow()
    {
        //��Ұ����
        zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; // ��ȡ���ֹ�����
        if (zoom != 0) // ����й���
        {
            offset -= zoom * offset;
        }
        //��ͷ����
        transform.LookAt(GetHeadPos(_target.playerObj.transform.position));

        transform.position = GetHeadPos(_target.playerObj.transform.position) - offset;

    }

    //������ת��������ת����:


    public bool isRotating, lookup;
    float mousex, mousey;

    Vector3 GetHeadPos(Vector3 playerPos)
    {
        return new Vector3(playerPos.x, playerPos.y + 1.5f, playerPos.z);
    }
    void Rotate()
    {
        /*if (Input.GetMouseButtonDown(1))//��������Ҽ�
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }*/
        isRotating = true;
        if (isRotating)
        {
            //�õ����x�����ƶ�����
            mousex = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            //��ת���λ����Ŀ�����崦����������������ϵ��y��

            transform.RotateAround(GetHeadPos(_target.playerObj.transform.position), Vector3.up, mousex);
            //ÿ����ת�����ƫ����
            offset = GetHeadPos(_target.playerObj.transform.position) - transform.position;
        }
    }
    void Rollup()
    {
        /*if (Input.GetMouseButtonDown(2))//��������м�
        {
            lookup = true;
        }
        if (Input.GetMouseButtonUp(2))
        {
            lookup = false;
        }*/
        lookup = true;
        if (lookup)
        {
            //�õ����y�����ƶ�����
            mousey = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            //��ת���λ����Ŀ�����崦���������������x��
            if (Mathf.Abs(transform.rotation.x + mousey-initialTransform.rotation.x) > 90) mousey = 0;
            transform.RotateAround(GetHeadPos(_target.playerObj.transform.position), transform.right, mousey);
            //ÿ����ת�����ƫ����
            offset = GetHeadPos(_target.playerObj.transform.position) - transform.position;
        }

    }
    void Move()
    {
        CameraRotate();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // Move when "w a s d" is pressed
        if (Mathf.Abs(vertical) > 0.01)
        {
            Vector3 fowardVector = transform.forward;
            fowardVector = new Vector3(fowardVector.x, 0, fowardVector.z).normalized;
            // move forward
            transform.Translate(MoveSpeed * Time.deltaTime * vertical * fowardVector, Space.World);
        }
        if (Mathf.Abs(horizontal) > 0.01)
        {
            Vector3 rightVector = transform.right;
            rightVector = new Vector3(rightVector.x, 0, rightVector.z).normalized;
            // move aside 
            transform.Translate(MoveSpeed * Time.deltaTime * horizontal * rightVector, Space.World);
        }

        // Fly up if space is clicked
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(MoveSpeed * Time.deltaTime * Vector3.up, Space.World);
        }
        // Fly down if left shift is clicked
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(MoveSpeed * Time.deltaTime * Vector3.down, Space.World);
        }

    }
    void CameraRotate()
    {
        if (Input.GetMouseButton(1))
        {
            float MouseX = Input.GetAxis("Mouse X");
            float MouseY = Input.GetAxis("Mouse Y");

            if ((Mathf.Abs(MouseX) > 0.01 || Mathf.Abs(MouseY) > 0.01))
            {
                transform.Rotate(new Vector3(0, MouseX * RotateSpeed * Time.deltaTime, 0), Space.World);

                float rotatedPitch = transform.eulerAngles.x - MouseY * RotateSpeed * Time.deltaTime * 1f;
                if (Mathf.Abs(rotatedPitch > 180 ? 360 - rotatedPitch : rotatedPitch) < FreeMaxPitch)
                {
                    transform.Rotate(new Vector3(-MouseY * RotateSpeed * Time.deltaTime * 1f, 0, 0));
                }
                else
                {
                    if (transform.eulerAngles.x < 180)
                        transform.eulerAngles = new Vector3((FreeMaxPitch - 1e-6f), transform.eulerAngles.y, 0);
                    else
                        transform.eulerAngles = new Vector3(-(FreeMaxPitch - 1e-6f), transform.eulerAngles.y, 0);
                }
            }
        }
    }
}
