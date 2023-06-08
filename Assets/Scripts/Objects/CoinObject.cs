using UnityEngine;

public class CoinObject : MonoBehaviour
{
    public ParticleSystem coinParticle;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Constants.PlayerTag))
        {
            gameObject.SetActive(false);
            coinParticle.transform.SetParent(transform.parent);
            coinParticle.gameObject.SetActive(true);
            coinParticle.Play();
            PlayerManager.Instance.AddCoins(5);
            AudioManager.Instance.PlayCoin();
        }
    }
}