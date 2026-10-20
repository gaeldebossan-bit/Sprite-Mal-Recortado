using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterClothing : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Sprite spriteSemRoupa;

    private Image imagem;
    private bool estaVestido = false;
    private DraggableClothing roupaAtual;

    private void Awake()
    {
        imagem = GetComponent<Image>();
    }

    public void Vestir(Sprite novoSprite, DraggableClothing roupa)
    {
        if (roupaAtual != null)
        {
            roupaAtual.Esconder();
        }

        if (imagem != null && novoSprite != null)
        {
            imagem.sprite = novoSprite;
            estaVestido = true;
            roupaAtual = roupa;

            roupa.Esconder();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (estaVestido)
        {
            imagem.sprite = spriteSemRoupa;
            estaVestido = false;

            if (roupaAtual != null)
            {
                RectTransform personagemRect = GetComponent<RectTransform>();

                roupaAtual.VoltarParaArmario(
                    personagemRect.anchoredPosition
                );

                roupaAtual = null;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // O personagem não se move.
    }
}