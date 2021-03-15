using DivinityGaz.InventorySystem;
using DivinityGaz.Managers;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Animator))]
public class InputSystem : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;

    Movement moveScript;

    [System.Serializable]
    public class InputSettings
    {
        public string forwardInput = "Vertical";
        public string strafedInput = "Horizontol";
        public string sprintdInput = "Sprint";
        public string aim = "Fire2";
        public string fire = "Fire1";
    }
    [SerializeField] public InputSettings input;

    [Space]
    [Header("Aiming SettingsQQ")]
    RaycastHit hit;
    public LayerMask aimlayers;
    Ray ray;
    bool hitDetected;

    [Header("Spine Settings")]
    public Transform spine;
    public Vector3 spineOffsetWithBow;

    [Header("Head Rotation Settings")]
    public float lookAtpoint = 2.8f;

    [Header("Camera & Character Syncing")]
    public float lookDistance = 5;
    public float lookSpeed = 5;
    Transform mainCamera;
    Transform camCenter;

    bool isAiming;

    bool isAbleToAim
    {
        get
        {
            if (equipedSlot == 0 || weapons[equipedSlot].IsMelee)
            {
                return false;
            }

            return true;
        }
    }

    bool isUsingFist
    {
        get
        {
            if (equipedSlot == 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
    public Bow bowScript;
    public Axe axeScript;

    public bool testAim;
    public float turnSpeed = 15f;

    [Header("KeyInput")]
    [SerializeField] Vector2 inputVector = new Vector2();
    [SerializeField] float frameInputValue = .05f;

    [Header("Fire Deplay Time")]
    float counter = 1f;
    bool enableArrow = true;

    Animator playerAnim;

    [Header("Switch BetweenWeapons")]
    [SerializeField] private int totalNumberOfSlotsIDs = 2;
    [SerializeField] private int currentWeaponID = 0;
    [SerializeField] private SwitchStates switchStates;

    public enum SwitchStates
    {
        fist,
        axe,
        bow
    }



    void Start ()
    {
        weapons[1] = axeScript;
        weapons[2] = bowScript;
        moveScript = GetComponent<Movement>();
        mainCamera = Camera.main.transform;
        camCenter = Camera.main.transform.parent;
        playerAnim = GetComponent<Animator>();

        //Cursor visibilty
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update ()
    {
        isAiming = Input.GetButton(input.aim);
        if (testAim)
            isAiming = true;

        if (Input.GetKey(KeyCode.Alpha1))
        {
            SwitchSatesFunction(0);
        } else if (Input.GetKey(KeyCode.Alpha2))
        {
            SwitchSatesFunction(1);
        } else if (Input.GetKey(KeyCode.Alpha3))
        {
            SwitchSatesFunction(2);
        }

        // exprimation
        Move();
        moveScript.AnimationCharacter(inputVector.y,
            inputVector.x);
        moveScript.sprintCharacter(Input.GetButton(input.sprintdInput));
        moveScript.CharacterAim(isAiming && isAbleToAim);

        //FireRtae
        var timeBetweenShoot = bowScript.bowSetting.fireRate;

        if (isAiming && isAbleToAim)
        {
            Aim();
            if (enableArrow)
            {
                EnableArrow();
                enableArrow = false;
            }
            if (counter > timeBetweenShoot)
            {
                moveScript.CharacterPullString(Input.GetButton(input.fire));
                if (Input.GetButtonUp(input.fire))
                {

                    moveScript.CharacterFireArrow();
                    if (hitDetected)
                    {
                        weapons[equipedSlot].Fire(hit.point);
                        counter = 0;
                    } else
                    {
                        weapons[equipedSlot].Fire(ray.GetPoint(300f));
                        counter = 0;
                    }
                }

            } else
            {
                counter += Time.deltaTime;
            }
        } else
        {
            AxeAttack();
            //FistAttack();
            bowScript.RemoveCrossHair();
            DisableArrow();
            Release();
            enableArrow = true;
        }

    }


    void Move ()
    {
        if (gameManager.IsAbleToMove == false) { return; }

        bool movedYThisFrame = false;
        bool movedXThisFrame = false;
        bool leftArrow = Input.GetKey(InputManager.IM.left);
        bool rightArrow = Input.GetKey(InputManager.IM.right);
        bool upArrow = Input.GetKey(InputManager.IM.forward);
        bool downArrow = Input.GetKey(InputManager.IM.backward);

        if (upArrow)
        {
            inputVector += Vector2.up * frameInputValue;
            movedYThisFrame = true;
        }
        if (downArrow)
        {
            inputVector += Vector2.down * frameInputValue;
            movedYThisFrame = true;
        }
        if (rightArrow)
        {
            inputVector += Vector2.right * frameInputValue;
            movedXThisFrame = true;
        }
        if (leftArrow)
        {
            inputVector += Vector2.left * frameInputValue;
            movedXThisFrame = true;
        }

        inputVector.x = FixAxis(inputVector.x, movedXThisFrame);
        inputVector.y = FixAxis(inputVector.y, movedYThisFrame);
    }

    private void FixedUpdate ()
    {
        if (Input.GetAxis(input.forwardInput) != 0 || Input.GetAxis(input.strafedInput) != 0 || isAiming)
        {
            RotateToCamView();
        }
    }

    private float FixAxis (float delta, bool state)
    {
        if (!state)
        {
            if (delta > 0)
            {
                delta -= frameInputValue;

                if (delta < 0)
                {
                    delta = 0;
                }

            } else
            {
                delta += frameInputValue;

                if (delta > 0)
                {
                    delta = 0;
                }
            }
        } else
        {
            if (delta > 1)
            {
                delta = 1;
            } else if (delta < -1)
            {
                delta = -1;
            }
        }

        return delta;
    }

    void LateUpdate ()
    {
        if (isAiming && isAbleToAim)
        {
            RotateCharacterSprin(spineOffsetWithBow);
        }
    }

    //Does the aiming and sends a raycast to target
    void Aim ()
    {
        Vector3 camPosition = mainCamera.position;
        Vector3 dir = mainCamera.forward;

        ray = new Ray(camPosition, dir);
        if (Physics.Raycast(ray, out hit, 500f, aimlayers))
        {
            hitDetected = true;
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            bowScript.ShowCrossHaire(hit.point);
        } else
        {
            hitDetected = false;
            bowScript.RemoveCrossHair();
        }
    }

    void RotateToCamView ()
    {
        Vector3 camCenterPos = camCenter.position;

        Vector3 lookPoint = camCenterPos + (camCenter.forward * lookDistance);
        Vector3 direction = lookPoint - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        Quaternion finalRotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
        transform.rotation = finalRotation;
    }

    void RotateCharacterSprin (Vector3 vector3)
    {
        spine.LookAt(ray.GetPoint(50));
        spine.Rotate(vector3);
    }

    public void Pull () => bowScript.PullString();

    public void EnableArrow () => bowScript.PickArrow();

    public void DisableArrow () => bowScript.DisableArrow();

    public void Release () => bowScript.ReleaseString();

    // Switch Between Weapons Handler
    #region Equipement

    [SerializeField] DefaultWeapon[] weapons = new DefaultWeapon[3];
    public int equipedSlot = 0;

    public void SwitchSatesFunction (WeaponItem weaponItem)
    {
        if (weapons[equipedSlot] == weaponItem) { return; }

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == weaponItem)
            {
                SwitchSatesFunction(i);
                break;
            }
        }
    }

    private void SwitchSatesFunction (int index)
    {
        if (equipedSlot == index) { return; }

        if (equipedSlot != 0)
        {
            weapons[equipedSlot].UnEquipWeapon();
        }

        if (index != 0)
        {
            weapons[index].EquipWeapon();
        }

        equipedSlot = index;
    }
    #endregion

    #region Axe

    private void AxeAttack ()
    {
        if (equipedSlot == 1)
        {
            if (Input.GetButton(input.fire))
                moveScript.CharacterAttackWithAxe(true);
        }
    }

    #endregion

    #region Fist

    private void FistAttack ()
    {
        if (equipedSlot == 0)
        {
            if (Input.GetButton(input.fire))
                moveScript.CharacterFistAttack(true);
            //Attack withFist animation
        }
    }
    #endregion
}
