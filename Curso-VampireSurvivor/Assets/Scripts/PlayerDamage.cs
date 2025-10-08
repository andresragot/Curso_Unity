using UnityEngine;

public class PlayerDamage : MonoBehaviour, IDamageable
{

    [SerializeField] float _health = 1000;
    [SerializeField] float _maxHealth = 1000;

    [SerializeField] GameObject lifeBar;

    bool is_dead = false;

    PlayerAudioManager audioPlayer;

    private void Awake()
    {
        audioPlayer = GetComponent<PlayerAudioManager>();
    }

    public float Health
    {
        get
        {
            return _health;
        }

        set
        {
            if (value <= _maxHealth && 0.0f <= value)
            {
                _health = value;
            }
            else if (0.0f > value)
            {
                _health = 0.0f;
                if (!is_dead)
                {
                    float time_to_die = audioPlayer.player_death();
                    is_dead = true;
                    Destroy(gameObject, time_to_die);
                }
            }
            else if (_maxHealth < value)
            {
                _health = _maxHealth;
            }

            float health_ui = Mathf.Abs(_health/_maxHealth);
            lifeBar.transform.localScale =
                new Vector3 (health_ui,
                lifeBar.transform.localScale.y,
                lifeBar.transform.localScale.z
                            );
        }

    }

    public void TakeDamage (Damage dmg)
    {
        Health -= dmg.amount;
    }

    public void RecoverDamage (float amount)
    {
        Health += amount;
    }
}
