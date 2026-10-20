using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableClothing : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Sprite spriteMarcelo;
    public Sprite spriteAssolan;
    public Sprite spriteHappy;

    private RectTransform rectTransform;
    private Canvas canvas;
    private Image imagem;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        imagem = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        imagem.raycastTarget = false;

        transform.SetAsLastSibling();

        gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition +=
            eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject alvo = eventData.pointerCurrentRaycast.gameObject;

        if (alvo != null)
        {
            CharacterClothing personagem =
                alvo.GetComponent<CharacterClothing>();

            if (personagem == null)
                personagem = alvo.GetComponentInParent<CharacterClothing>();

            if (personagem != null)
            {
                if (personagem.name == "Marcelo")
                {
                    personagem.Vestir(spriteMarcelo, this);
                }
                else if (personagem.name == "Assolan")
                {
                    personagem.Vestir(spriteAssolan, this);
                }
                else if (personagem.name == "Happy")
                {
                    personagem.Vestir(spriteHappy, this);
                }

                imagem.raycastTarget = true;
                return;
            }
        }

        // Se soltou no vazio, permanece onde foi solta.
        imagem.raycastTarget = true;
    }

    public void Esconder()
    {
        gameObject.SetActive(false);
    }

    public void VoltarParaArmario(Vector2 novaPosicao)
    {
        gameObject.SetActive(true);

        rectTransform.anchoredPosition = novaPosicao;

        imagem.raycastTarget = true;
    }
}