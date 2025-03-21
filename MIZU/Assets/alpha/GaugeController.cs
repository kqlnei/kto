using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class GaugeController : MonoBehaviour
{
    [SerializeField] private GameObject _gauge;
    [SerializeField] private int _HP;
    private float _HP1;

    public GameObject managerObject;  // ModeManagerがついているオブジェクト
    private ModeManager _modeManager;
    private CollisionManager _collisionManager;
    private GameObject player1Object;
    private GameObject player2Object;

    public enum Player { Player1, Player2 }
    public Player player;

    public float water = 1;
    public float ice = 1;
    public float cloud = 1;
    public float slime = 1;
    
    bool isOnGround = false;
    bool isOnWater = false;
    //private bool isDead = false;

    private List<string> allowedTags = new List<string> { "HealSpot", "Ground" };

    [SerializeField]
    private MM_GroundCheck _groundCheck;

    void Start()
    {
        _modeManager = managerObject.GetComponent<ModeManager>();
        _collisionManager = managerObject.GetComponent<CollisionManager>();
        _HP1 = _gauge.GetComponent<RectTransform>().sizeDelta.x / _HP;

        player1Object = _modeManager.Player1;
        player2Object = _modeManager.Player2;

        if (_groundCheck == null)
            Debug.LogWarning($"{nameof(_groundCheck)}がアタッチされていません");

    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    void Update()
    {
        
        //if (!isDead)
        //{
            string currentModelTag = (player == Player.Player1) ? _modeManager.player1ModelTag : _modeManager.player2ModelTag;

            // ダメージ処理
            float attackPower = 0.1f;
            BeInjured(attackPower, currentModelTag);

            // プレイヤー1とプレイヤー2のヒールスポット衝突チェック
            if (player == Player.Player1)
            {
                // プレイヤー1がヒールスポットに衝突したかチェック
                foreach (Collider col in _collisionManager.GetPlayer1HitColliders())
                {
                    if (isOnGround || isOnWater || allowedTags.Contains(col.gameObject.tag))
                    //if(allowedTags.Contains(col.gameObject.tag))
                    {
                        Heal(100f);  // プレイヤー1の回復量
                        break;
                    }
                }
            }
            else if (player == Player.Player2)
            {
                // プレイヤー2がヒールスポットに衝突したかチェック
                foreach (Collider col in _collisionManager.GetPlayer2HitColliders())
                {
                    if (isOnGround || isOnWater || allowedTags.Contains(col.gameObject.tag))
                    //if(allowedTags.Contains(col.gameObject.tag))
                {
                        Heal(100f);  // プレイヤー2の回復量
                        break;
                    }
                }
            }
        }
    //}

    private void GroundCheck()
    {
        isOnGround = _groundCheck.IsGround();
        isOnWater = _groundCheck.IsPuddle();
    }

    public void BeInjured(float attack, string modelTag)
    {
        float damage = 0;

        switch (modelTag)
        {
            case "Water":
                damage = _HP1 * attack * water;
                break;
            case "Ice":
                damage = _HP1 * attack * ice;
                break;
            case "Cloud":
                damage = _HP1 * attack * cloud;
                break;
            case "Slime":
                damage = _HP1 * attack * slime;
                break;
        }

        StartCoroutine(DamageCoroutine(damage));
    }

    IEnumerator DamageCoroutine(float damage)
    {
        // 時間経過に基づくダメージ減少速度を調整
        float damagePerFrame = damage / 1.0f; // ダメージを減少させる速度を調整（1秒間に減る量）

        // ダメージを減少させる処理
        Vector2 currentSize = _gauge.GetComponent<RectTransform>().sizeDelta;
        currentSize.x -= damagePerFrame*Time.deltaTime;

        // 体力が0以下になった場合、体力を0に設定
        if (currentSize.x <= 0)
        {
            currentSize.x = 0;
            Debug.Log(player + " is dead!");

            // プレイヤー1またはプレイヤー2に対応するMM_Test_Playerスクリプトを取得
            MM_Test_Player playerScript = null;

            if (player == Player.Player1 && player1Object != null)
            {
                playerScript = player1Object.GetComponent<MM_Test_Player>();
            }
            else if (player == Player.Player2 && player2Object != null)
            {
                playerScript = player2Object.GetComponent<MM_Test_Player>();
            }

            // playerScriptが取得できた場合、OnStateChangeLiquidを呼び出す
            if (playerScript != null)
            {
                playerScript.OnStateChangeLiquid();
            }
        }

        // ゲージのサイズを更新
        _gauge.GetComponent<RectTransform>().sizeDelta = currentSize;
        yield return null;
    }




    // 回復処理
    public void Heal(float healAmount)
    {
        float heal = _HP1 * healAmount;
        StartCoroutine(HealCoroutine(heal));
    }

    IEnumerator HealCoroutine(float heal)
    {
        Vector2 currentSize = _gauge.GetComponent<RectTransform>().sizeDelta;
        currentSize.x += heal;

        // ゲージが最大幅を超えないようにする
        float maxWidth = _HP1 * _HP;
        if (currentSize.x > maxWidth)
        {
            currentSize.x = maxWidth;
        }

        _gauge.GetComponent<RectTransform>().sizeDelta = currentSize;
        yield return null;
    }
}