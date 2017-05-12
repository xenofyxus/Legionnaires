using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Interface.BottomRowBar.KingUpgrades
{
	public class UpgradesPanelBehaviour:MonoBehaviour
	{
		[SerializeField]
		private float animationSpeed = 1f;

		private Image maskImage = null;
		private Vector2 originalPos;
		private Vector2 loweredPosition;
		private RectTransform rectTransform;

		private bool disabling = false;

		private void Awake()
		{
			rectTransform = transform as RectTransform;
			maskImage = GetComponent<Image>();
			originalPos = rectTransform.anchoredPosition;
			loweredPosition = new Vector2(originalPos.x, originalPos.y - rectTransform.rect.height);
		}

		private void OnEnable()
		{
			rectTransform.anchoredPosition = loweredPosition;
			maskImage.fillAmount = 0;
		}

		public void Enable()
		{
			disabling = false;
			gameObject.SetActive(true);
		}

		public void Disable()
		{
			disabling = true;
		}

		private void Update()
		{
			if (disabling)
			{
				if (rectTransform.anchoredPosition != loweredPosition)
				{
					rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, loweredPosition, (rectTransform.rect.height / animationSpeed) * Time.deltaTime);
					maskImage.fillAmount = 1 - (originalPos.y - rectTransform.anchoredPosition.y) / rectTransform.rect.height;
				}
				else
				{
					gameObject.SetActive(false);
				}
			}
			else if (rectTransform.anchoredPosition != originalPos)
			{
				rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, originalPos, (rectTransform.rect.height / animationSpeed) * Time.deltaTime);
				maskImage.fillAmount = 1 - (originalPos.y - rectTransform.anchoredPosition.y) / rectTransform.rect.height;
			}
		}
	}
}

