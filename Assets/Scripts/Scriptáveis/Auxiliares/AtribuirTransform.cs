using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtribuirTransform : MonoBehaviour
    {
        public VariavelTransform variavelTransform;

		private void OnEnable()
		{
			variavelTransform.valor = this.transform;
			Destroy(this);
		}

}
