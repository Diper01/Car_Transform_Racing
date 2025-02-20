/// <summary>
/// Custom font. Author Cesar Rios 2013
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent (typeof (MeshRenderer))]
[RequireComponent (typeof (MeshFilter))]

[ExecuteInEditMode]
public class EasyFontTextMesh : MonoBehaviour {

	public enum TEXT_ANCHOR 
	{
		UpperLeft,
		UpperRight,
		UpperCenter,
		MiddleLeft,
		MiddleRight,
		MiddleCenter,
		LowerLeft,
		LowerRight,
		LowerCenter
	}
	
	public enum TEXT_ALIGNMENT
	{
		left,
		right,
		center
	}
	
	
	/// <summary>
	/// Define if we are drawing the main font, the shadow or the outline
	/// </summary>
	private enum TEXT_COMPONENT 
	{
		Main,
		Shadow,
		Outline
	}
	
	[System.Serializable]
	public class TextProperties
	{
		public string 				text = "Hello World!";
		public Font					font;
		public Material 			customFillMaterial;
		public int					fontSize = 16;
		public float				size = 16;
		public int 					orderInLayer = 0;
		public int 					lineOfNum = 0;
		public TEXT_ANCHOR			textAnchor = TEXT_ANCHOR.MiddleCenter;
		public TEXT_ALIGNMENT		textAlignment;
		public float                spacing = 1;   
		public	float				lineSpacing = 1;
		public	Color				fontColorTop = new Color(1,1,1,1);
		public	Color				fontColorBottom = new Color(1,1,1,1);
		public 	bool				enableShadow;
		public 	Color				shadowColor = new Color(0,0,0,1);
		public  Vector3				shadowDistance = new Vector3(0,-1,0);
		public 	bool				enableOutline;
		public 	Color				outlineColor = new Color(0,0,0,1);
		public 	float				outLineWidth = 0.3f; 
		public	bool				highQualityOutline = false;
		
	}
	
	/// <summary>
	/// DO NOT CHANGE THIS DIRECTLY
	/// </summary>
	//[HideInInspector]
	public TextProperties 	_privateProperties;  //WARNING!: do not change it directly
	
	#region properties
	
	
	// If you have some problematic text that appears corrupt when enabling it, try to enable this variable
	public bool				updateAlwaysOnEnable = true; 
	
	
	/// <summary>
	/// For complex setups with a lot of materials override the auto setting may not be usefull. WARNING!: You will 
	/// have to setup all the materials by hand.
	/// </summary>
	public bool				dontOverrideMaterials;

    /// <summary>
    /// Gets or sets the text to show
    /// </summary>
    public bool isDirty;
    LocalisedString LocalisedString;
    public	string			text
	{
		get { return 		_privateProperties.text;}
		set {
            //if (LineOfNum > 10)
            //{
            //    _privateProperties.text = value;
            //    _privateProperties.text = TextLayer.RebuildString(value, LineOfNum);
            //    _privateProperties.text = TextLayer.WarpWord(value.Replace("\n", ""), LineOfNum);
            //}
            //else
                //string txt = value;
                _privateProperties.text = value.Trim();
            //isDirty = true;
            //RefreshMeshEditor();
            RefreshMesh(true);
        }
	}
	
	/// <summary>
	/// Gets or sets the Font Type
	/// </summary>
	public	Font			FontType
	{
		get { return _privateProperties.font;}
		set { _privateProperties.font = value; ChangeFont();}
	}
	
	/// <summary>
	/// Gets or sets the filling material (for having patterns inside the letters) 
	/// </summary>
	public	Material		CustomFillMaterial
	{
		get { return _privateProperties.customFillMaterial;}
		set { _privateProperties.customFillMaterial = value;isDirty = true; }
	}
	
	/// <summary>
	/// Gets or sets the font size. This will increase the resolution of the text and the font texure size
	/// </summary>
	public	int				FontSize
	{
		get { return _privateProperties.fontSize;}
		set { _privateProperties.fontSize = value; }
	}
	
	
	/// <summary>
	/// Gets or sets the size of the letters. The proportional quad size for the letters
	/// </summary>
	public	float			Size
	{
		get { return _privateProperties.size;}
		set { _privateProperties.size = value; }
	}

	public int 			OrderInLayer
	{
		get{return _privateProperties.orderInLayer;}
		set{_privateProperties.orderInLayer = value; isDirty = true; }
	}

	public int   	LineOfNum
	{
		get{return _privateProperties.lineOfNum;}
		set{_privateProperties.lineOfNum = value;}
	}
	
	/// <summary>
	/// Gets or sets the Text anchor
	/// </summary>
	public	TEXT_ANCHOR		Textanchor
	{
		get { return _privateProperties.textAnchor;}
		set { _privateProperties.textAnchor = value; }
	}
	
	/// <summary>
	/// Gets or sets the text alignment. Only for paragraphs
	/// </summary>
	public	TEXT_ALIGNMENT	Textalignment
	{
		get { return _privateProperties.textAlignment;}
		set { _privateProperties.textAlignment = value; }
	}

	/// <summary>
	/// 字间距
	/// </summary>
	/// <value>The spacing.</value>
	public float Spacing
	{
		get {return _privateProperties.spacing;}
		set { _privateProperties.spacing =value;}
	}

	/// <summary>
	/// Gets or sets the space between lines of a paragraph
	/// </summary>
	public	float			LineSpacing
	{
		get { return _privateProperties.lineSpacing;}
		set { _privateProperties.lineSpacing = value; }
	}
	
	/// <summary>
	/// Gets or sets the top font color
	/// </summary>
	public	Color			FontColorTop
	{
		get { return _privateProperties.fontColorTop;}
		set { _privateProperties.fontColorTop = value; SetColor(_privateProperties.fontColorTop,_privateProperties.fontColorBottom);}
	}
	
	/// <summary>
	/// Gets or sets the bottom font color
	/// </summary>
	public	Color			FontColorBottom
	{
		get { return _privateProperties.fontColorBottom;}
		set { _privateProperties.fontColorBottom = value; SetColor(_privateProperties.fontColorTop,_privateProperties.fontColorBottom);}
	}
	
	/// <summary>
	/// Enable or deisable proyected shadow. This will draw the text twice
	/// </summary>
	public	bool			EnableShadow
	{
		get { return _privateProperties.enableShadow;}
		set { _privateProperties.enableShadow = value; }
	}
	
	/// <summary>
	/// Gets or sets the shadow color
	/// </summary>
	public	Color			ShadowColor
	{
		get { return _privateProperties.shadowColor;}
		set { _privateProperties.shadowColor = value; SetShadowColor(_privateProperties.shadowColor);}
	}
	
	/// <summary>
	/// Gets or sets the shadow offset distance. Note: Normally you don't want to change the Z coordinate.
	/// </summary>
	public	Vector3			ShadowDistance
	{
		get { return _privateProperties.shadowDistance;}
		set { _privateProperties.shadowDistance = value; }
	}
	
	/// <summary>
	/// Enable or disable the font outline. This will draw the text 4 times more
	/// </summary>
	public	bool			EnableOutline
	{
		get { return _privateProperties.enableOutline;}
		set { _privateProperties.enableOutline = value; }
	}
	
	/// <summary>
	/// Gets or sets the outline color
	/// </summary>
	public	Color			OutlineColor
	{
		get { return _privateProperties.outlineColor;}
		set { _privateProperties.outlineColor = value; SetOutlineColor(_privateProperties.outlineColor);}
	}
	
	/// <summary>
	/// Gets or sets the outline width
	/// </summary>
	public	float			OutLineWidth
	{
		get { return _privateProperties.outLineWidth;}
		set { _privateProperties.outLineWidth = value; }
	}
	
	// Gets or set the high quality outline. It increases the vertex count
	
	
	public	bool			HighQualityOutline
	{
		get { return _privateProperties.highQualityOutline;}
		set { _privateProperties.highQualityOutline = value; }
	}
		
	#endregion
		
	
	
	#region Private vars
	
	//Cache vars
	private Mesh			textMesh;
	private MeshFilter		textMeshFilter;
	private Material		fontMaterial;
	private Renderer		textRenderer;
	private char[] 			textChars;
	

	/// <summary>
	/// The current line break.
	/// </summary>
	private int currentLineBreak = 0;
	
	/// <summary>
	/// The accumulate height of the paragraph
	/// </summary>
	private float heightSum;
	
	/// <summary>
	/// Store in wich character there is a line break
	/// </summary>
	private List<int> lineBreakCharCounter 			= new List<int>(); 
	
	/// <summary>
	/// The distance between characters accumulated before each line break
	/// </summary>
	private List<float> lineBreakAccumulatedDistance 	= new List<float>(); 
	
	
	//Mesh data
	Vector3[]				vertices;
	int[]					triangles;
	Vector2[]				uv;
	Vector2[]				uv2;
	Color[]					colors;
	
	#endregion
	
	[HideInInspector]
	public bool GUIChanged = false;
	
	#region special char codes
	private char	LINE_BREAK = Convert.ToChar(10);  //This is the character code for the alt+enter character that Unity includes in the text
    #endregion

    //public string originalString;
	void Awake()
    {
        //originalString = _privateProperties.text;
        if (!Application.isPlaying) return;
//		#if UNITY_EDITOR
//			if (_privateProperties == null)
//				_privateProperties = new TextProperties();
		
//			if (_privateProperties.font == null)
//				_privateProperties.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
//#endif



      //  _privateProperties.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        CacheTextVars();

        //if (GetComponent<LocalisedString>() == null)
        //{
        //    LocalisedString = gameObject.AddComponent<LocalisedString>();
        //}
        //else
        //{
        //    LocalisedString = GetComponent<LocalisedString>();
        //}
        Font.textureRebuilt += FontTexureRebuild;
        //isDirty = true;
        //RefreshMeshEditor();
        //RefreshMesh(true);

    }
    private void Start()
    {
        //RefreshLocalisedString();
        //LocalisedString.UpdateString();
        //RefreshMesh(true);
        //text = Localisation.GetString(originalString);
        isDirty = true;
    }


    void OnEnable()
	{
        //_privateProperties.font.textureRebuildCallback += FontTexureRebuild; //Register to the texture change event
        //Font.textureRebuilt += FontTexureRebuild;
        //GetComponent<LocalisedString>().UpdateString();
        //RefreshMeshEditor();
        //if (updateAlwaysOnEnable)
        //RefreshMesh(true);
        //if (GetComponent<LocalisedString>() != null)
        //RefreshMeshEditor();

    }
	
	
	
	/// <summary>
	/// Initialize the text variables
	/// </summary>
	public void CacheTextVars()
	{
		
		textMeshFilter = GetComponent<MeshFilter>();
		
		if (textMeshFilter == null)
			textMeshFilter 	= gameObject.AddComponent<MeshFilter>();
		
		//Setup renderer objects
		textMesh = textMeshFilter.sharedMesh;
		
		if (textMesh == null)
		{
			textMesh 		= new Mesh {name = gameObject.name + GetInstanceID().ToString() };//Rename to something
			textMeshFilter.sharedMesh = textMesh;
		}
		
		textRenderer = GetComponent<Renderer>();
		
		if (textRenderer == null)
			textRenderer = 	  gameObject.AddComponent<MeshRenderer>();
		
		//Set materials
		if (!dontOverrideMaterials)
		{
			if (_privateProperties.customFillMaterial != null)
			{
				if (_privateProperties.enableShadow || _privateProperties.enableOutline)
				{
					if (textRenderer.sharedMaterials.Length < 2)
						textRenderer.sharedMaterials = new Material[2]{_privateProperties.font.material , _privateProperties.customFillMaterial};
					
					_privateProperties.customFillMaterial.mainTexture = _privateProperties.font.material.mainTexture;
					textRenderer.sharedMaterial =  _privateProperties.font.material;
				}
				else
				{
					_privateProperties.customFillMaterial.mainTexture = _privateProperties.font.material.mainTexture;
					textRenderer.sharedMaterial = _privateProperties.customFillMaterial;
				}
			}
			else
			{
				if (textRenderer.sharedMaterials == null)
					textRenderer.sharedMaterials = new Material[1]{_privateProperties.font.material};
				else
				{
					textRenderer.sharedMaterials = new Material[1]{textRenderer.sharedMaterial};
				}
				
			}
		}
		
		
	}

    string localised = "";
    

	/// <summary>
	/// Refreshs the mesh.
	/// </summary>
	/// <param name='_updateTexureInfo'>
	/// _update texure info. 
	/// </param>
    
    //public void RefreshLocalisedString()
    //{
    //    if (LocalisedString != null)
    //    {
    //        //text = Localisation.GetString(originalString);
    //        LocalisedString.UpdateString();
    //        //RefreshMesh(true);
    //    }
    //}

    public void RefreshMesh(bool _updateTexureInfo)
	{
       
        //Debug.Log("Refreshed Mesh");
        //Update texture
        if (_updateTexureInfo)
			_privateProperties.font.RequestCharactersInTexture(_privateProperties.text, _privateProperties.fontSize);


        textChars = null;
		textChars =  _privateProperties.text.ToCharArray();

        /////////////////////////////////////if (GetComponent<LocalisedString>() != null)


        AnalizeText(); //Check for special characters



        //The vertex count must increase if we are going to use high quality outline
        int highQualityOutlineModifier = 0;
		
		if (_privateProperties.highQualityOutline)
			highQualityOutlineModifier = 4;
		
		//If we use shadow or/and outline we have to change the number of vertex, triangles...
		int meshOptionMultipler = 1;
		
		if (_privateProperties.enableShadow && _privateProperties.enableOutline)
			meshOptionMultipler = 6 + highQualityOutlineModifier;
		else if (_privateProperties.enableOutline)
			meshOptionMultipler = 5 + highQualityOutlineModifier;
		else if (_privateProperties.enableShadow)
			meshOptionMultipler = 2;
		
	
		vertices 	= new Vector3[textChars.Length*4*meshOptionMultipler];
		triangles 	= new int[textChars.Length*6*meshOptionMultipler];
		uv			= new Vector2[textChars.Length*4*meshOptionMultipler];
		uv2 		= new Vector2[textChars.Length*4*meshOptionMultipler];
		colors		= new Color[textChars.Length*4*meshOptionMultipler];



        int characterPosition = 0;
		int alignmentPass = 0;
		
		//Shadow
		if (_privateProperties.enableShadow)
		{
			ResetHelperVariables();
            for (int i = 0; i < textChars.Length; i++)
            {
                CreateCharacter(textChars[i], characterPosition, _privateProperties.shadowDistance, _privateProperties.shadowColor, _privateProperties.shadowColor);
                characterPosition++;
            }
   //         foreach (char iteratorChar in textChars)
			//{
				
			//}
			SetAlignment(alignmentPass++);
		}

        //Outline


        if (_privateProperties.enableOutline)
		{
			float angleIncrement = 90;
			
			if (_privateProperties.highQualityOutline)
				angleIncrement = 45; 
			
			for(float ang = 0.0f; ang < 360.0f; ang += angleIncrement) 
			{
				Vector3 dir = Vector3.right;
				dir.x = Mathf.Cos(ang * Mathf.Deg2Rad);
				dir.y = Mathf.Sin(ang * Mathf.Deg2Rad);
				
				ResetHelperVariables();
                for (int i = 0; i < textChars.Length; i++)
                {
                    CreateCharacter(textChars[i], characterPosition, dir * _privateProperties.outLineWidth, _privateProperties.outlineColor, _privateProperties.outlineColor);
                    characterPosition++;
                }
				//foreach (char iteratorChar in textChars) 
				//{
					
				//}
				SetAlignment(alignmentPass++);
			}
			
			
		}
		
		
		//Normal text
		ResetHelperVariables();
        for (int i = 0; i < textChars.Length; i++)
        {
            CreateCharacter(textChars[i], characterPosition, Vector3.zero, _privateProperties.fontColorTop, _privateProperties.fontColorBottom);
            characterPosition++;
        }
  //      foreach (char iteratorChar in textChars)
		//{
			
		//}
		SetAlignment(alignmentPass++);

        //Asign mesh data

        if (textMesh != null)
		{
            
            textMesh.Clear(true);
			SetAnchor();
	     	textMesh.vertices = vertices;
			textMesh.uv 		= uv;
			//SetUV2();
			textMesh.uv2		= uv2;
			
			if (_privateProperties.customFillMaterial != null && (_privateProperties.enableShadow || _privateProperties.enableOutline))
				SetTrianglesForMultimesh();
			else
	        	textMesh.triangles 	= triangles;
			textMesh.colors		= colors;
            
        }

        // Order In Layer
        if (textRenderer != null)
		{
			textRenderer.sortingOrder = OrderInLayer;
		}
    }

	
	void ResetHelperVariables()
	{
	 	lineBreakAccumulatedDistance.Clear();
		lineBreakCharCounter.Clear();
		currentLineBreak 	= 0;
	 	heightSum 			= 0;	
	}
	
	
	/// <summary>
	/// Analizes the text for keycodes and prepare it for rendering. Right now only \n is supported
	/// </summary>
	void AnalizeText()
	{		

		//Test characters for know keycodes
		bool recheckCharArray = true;
		while (recheckCharArray)
		{
			recheckCharArray = false;
			
			for (int i = 0; i < textChars.Length ; i++)
			{
				if (textChars[i] == '\\' && i+1 < textChars.Length && textChars[i+1] == 'n') //Check for \n for a line break
				{
					
					char[] tempCharArray = new char[textChars.Length -1];
					
					int k = 0;
					
					for (int j = 0; j < textChars.Length ; j++)
					{						
						if (j == i)
						{
							tempCharArray[k] = LINE_BREAK;
							k++;
							continue;
						}
						else if (j == i+1) //Jump this character
						{	
							j++;
							if (j >= textChars.Length)
								continue;
						}
						
						tempCharArray[k] = textChars[j];
						
						k++;
					}
					
					textChars = tempCharArray;
					recheckCharArray = true;
					break;
				}
			}
		}
		
	}
	/// <summary>
	/// Creates the character.
	/// </summary>
	/// <param name='_character'>
	/// The character to draw
	/// </param>
	/// <param name='_arrayPosition'>
	/// The position in the text
	/// </param>
	/// <param name='_offset'>
	/// Do we have to draw it with an offset? (Used in ourline and shado2)
	/// </param>
	/// <param name='_colorTop'>
	/// _color top.
	/// </param>
	/// <param name='_colorBottom'>
	/// _color bottom.
	/// </param>
	void CreateCharacter(char _character, int _arrayPosition, Vector3 _offset, Color _colorTop, Color _colorBottom )
	{
		if (lineBreakAccumulatedDistance.Count == 0)
			lineBreakAccumulatedDistance.Add(0);
		
		if (lineBreakCharCounter.Count == 0)
			lineBreakCharCounter.Add(0);

		
		CharacterInfo charInfo = new CharacterInfo();		
		
		if (!_privateProperties.font.GetCharacterInfo(_character,out charInfo,_privateProperties.fontSize))
		{
			lineBreakCharCounter.Add(lineBreakCharCounter[currentLineBreak]);
			lineBreakAccumulatedDistance.Add(0);
			currentLineBreak++;
			return;
		}
		
		lineBreakCharCounter[currentLineBreak]++;
		
		//print("Character: " + _character  +" Vertex: " +   charInfo.vert + "UV: " +charInfo.uv + "Char size: " + charInfo.size + "Char width: " + charInfo.width + "  Is flipped: "+ charInfo.flipped);
	
		float 	sizeModifier = _privateProperties.size/_privateProperties.fontSize;
		_offset *= _privateProperties.size*0.1f;
		float 	characterWidth 			= charInfo.vert.width 	* sizeModifier;		
		float 	characterHeight 		= charInfo.vert.height 	* sizeModifier;
	
		Vector2 betweenLefttersOffset 	= new Vector2(charInfo.vert.x,charInfo.vert.y)*sizeModifier;
		
		//for vertical adjustments
		
		if (_character != ' ') //Don't acumulate height if its a space
			heightSum += (charInfo.vert.y+charInfo.vert.height*0.5f)*sizeModifier;

		Vector3 currentAcumulatedCharacterDistance = new Vector3(lineBreakAccumulatedDistance[currentLineBreak]*sizeModifier * _privateProperties.spacing ,- _privateProperties.size*currentLineBreak*_privateProperties.lineSpacing,0) ;
		
		//Create a quad with the size of the character
		if (charInfo.flipped == true)
		{	
			vertices[4*_arrayPosition] 		= new Vector3(betweenLefttersOffset.x + characterWidth  	,  characterHeight+betweenLefttersOffset.y 	,  0 ) + _offset + currentAcumulatedCharacterDistance;
			vertices[4*_arrayPosition+1] 	= new Vector3(betweenLefttersOffset.x    					,  characterHeight+betweenLefttersOffset.y 	,  0 ) + _offset + currentAcumulatedCharacterDistance;  
			vertices[4*_arrayPosition+2]	= new Vector3(betweenLefttersOffset.x   					,  betweenLefttersOffset.y				 	,  0 ) + _offset + currentAcumulatedCharacterDistance; 
			vertices[4*_arrayPosition+3]	= new Vector3(betweenLefttersOffset.x + characterWidth    	,  betweenLefttersOffset.y				 	,  0 ) + _offset + currentAcumulatedCharacterDistance;
		}
		
		else
		{
			vertices[4*_arrayPosition] 		= new Vector3(betweenLefttersOffset.x + characterWidth  ,  characterHeight+betweenLefttersOffset.y		,  0 ) + _offset + currentAcumulatedCharacterDistance;  
			vertices[4*_arrayPosition+1] 	= new Vector3(betweenLefttersOffset.x  					,  characterHeight+betweenLefttersOffset.y		,  0 ) + _offset + currentAcumulatedCharacterDistance; 
			vertices[4*_arrayPosition+2]	= new Vector3(betweenLefttersOffset.x  					,  betweenLefttersOffset.y  					,  0 ) + _offset + currentAcumulatedCharacterDistance;
			vertices[4*_arrayPosition+3]	= new Vector3(betweenLefttersOffset.x + characterWidth  ,  betweenLefttersOffset.y  					,  0 ) + _offset + currentAcumulatedCharacterDistance;
		}
		
		lineBreakAccumulatedDistance[currentLineBreak] += charInfo.width;
		charInfoWidth = Mathf.Max(charInfoWidth, charInfo.width);

		//Set triangles
		triangles[6*_arrayPosition] 	= _arrayPosition*4;
		triangles[6*_arrayPosition+1] 	= _arrayPosition*4+1;
		triangles[6*_arrayPosition+2] 	= _arrayPosition*4+2;
		triangles[6*_arrayPosition+3] 	= _arrayPosition*4;
		triangles[6*_arrayPosition+4] 	= _arrayPosition*4+2;
		triangles[6*_arrayPosition+5] 	= _arrayPosition*4+3;
		
		
		//Set UVs
		if (charInfo.flipped == true)
		{
			uv[4*_arrayPosition] 		=   new Vector2(charInfo.uv.x,charInfo.uv.y+charInfo.uv.height);
			uv[4*_arrayPosition+1] 		=   new Vector2(charInfo.uv.x,charInfo.uv.y);
			uv[4*_arrayPosition+2] 		=  new Vector2(charInfo.uv.x+charInfo.uv.width,charInfo.uv.y);  
			uv[4*_arrayPosition+3] 		=  new Vector2(charInfo.uv.x+charInfo.uv.width,charInfo.uv.y+charInfo.uv.height);
		}
		else
		{
			uv[4*_arrayPosition] 		= new Vector2(charInfo.uv.x+charInfo.uv.width,charInfo.uv.y); 
			uv[4*_arrayPosition+1] 		= new Vector2(charInfo.uv.x,charInfo.uv.y);
			uv[4*_arrayPosition+2] 		= new Vector2(charInfo.uv.x,charInfo.uv.y+charInfo.uv.height);
			uv[4*_arrayPosition+3] 		= new Vector2(charInfo.uv.x+charInfo.uv.width,charInfo.uv.y+charInfo.uv.height);
		}
		
		//Set uv2
		if (_privateProperties.customFillMaterial != null)  //Only if we need them
		{
			Vector2 uvOffset = new Vector2(_offset.x,_offset.y);
			Vector2 uvAccumulatedDistance = new Vector2(currentAcumulatedCharacterDistance.x,currentAcumulatedCharacterDistance.y);
			
			uv2[4*_arrayPosition] 			=   new Vector2(betweenLefttersOffset.x + characterWidth  ,  characterHeight+betweenLefttersOffset.y) + uvOffset + uvAccumulatedDistance;  
			uv2[4*_arrayPosition+1] 		=   new Vector2(betweenLefttersOffset.x    					,  characterHeight+betweenLefttersOffset.y) + uvOffset + uvAccumulatedDistance;  
			uv2[4*_arrayPosition+2] 		=  	new Vector2(betweenLefttersOffset.x   					,  betweenLefttersOffset.y) + uvOffset + uvAccumulatedDistance; 
			uv2[4*_arrayPosition+3] 		=  	new Vector2(betweenLefttersOffset.x + characterWidth    	,  betweenLefttersOffset.y) + uvOffset + uvAccumulatedDistance;
		}	
		
		//Set colors
		colors[4*_arrayPosition]		= _colorBottom;
		colors[4*_arrayPosition+1]		= _colorBottom;
		colors[4*_arrayPosition+2]		= _colorTop;
		colors[4*_arrayPosition+3]		= _colorTop;
		
	}
	

	float charInfoWidth = 0;//文字宽度

	/// <summary>
	/// Sets the anchor.
	/// </summary>
	void SetAnchor()
	{
		
		Vector2 textOffset = Vector2.zero;
		
		float maxDistance = 0;
				
		for (int i = 0; i< lineBreakAccumulatedDistance.Count; i++)
		{
			if (lineBreakAccumulatedDistance[i] > maxDistance)
				maxDistance = lineBreakAccumulatedDistance[i];
		}
		
		switch (_privateProperties.textAnchor)
		{
			case TEXT_ANCHOR.MiddleLeft:
			case TEXT_ANCHOR.UpperLeft:
			case TEXT_ANCHOR.LowerLeft:
				switch(_privateProperties.textAlignment)
				{
					case TEXT_ALIGNMENT.left:
						textOffset.x = 0;	
					break;
				
					case TEXT_ALIGNMENT.right:
				        textOffset.x = maxDistance*_privateProperties.size/_privateProperties.fontSize * _privateProperties.spacing;	
					break;
				
					case TEXT_ALIGNMENT.center:
				textOffset.x += maxDistance*0.5f*_privateProperties.size/_privateProperties.fontSize * _privateProperties.spacing;
				break;
				}
			
			
			break;
			
			case TEXT_ANCHOR.MiddleRight:
			case TEXT_ANCHOR.UpperRight:
			case TEXT_ANCHOR.LowerRight:
				switch(_privateProperties.textAlignment)
				{
					case TEXT_ALIGNMENT.left:
				textOffset.x -= maxDistance*_privateProperties.size/_privateProperties.fontSize * _privateProperties.spacing;
				break;
				
					case TEXT_ALIGNMENT.right:
				textOffset.x = 0;
				break;
				
					case TEXT_ALIGNMENT.center:
				textOffset.x -= maxDistance*0.5f*_privateProperties.size/_privateProperties.fontSize  * _privateProperties.spacing;;
				break;
				}

			textOffset.x += charInfoWidth*_privateProperties.size/_privateProperties.fontSize * _privateProperties.spacing - _privateProperties.size;
			break;
			
			case TEXT_ANCHOR.MiddleCenter:
			case TEXT_ANCHOR.UpperCenter:
			case TEXT_ANCHOR.LowerCenter:
				switch(_privateProperties.textAlignment)
				{
					case TEXT_ALIGNMENT.left:
				textOffset.x -= maxDistance*_privateProperties.size*0.5f/_privateProperties.fontSize * _privateProperties.spacing;;
				break;
				
					case TEXT_ALIGNMENT.right:
				textOffset.x = maxDistance*0.5f*_privateProperties.size/_privateProperties.fontSize * _privateProperties.spacing;;		
					break;
				
					case TEXT_ALIGNMENT.center:
						textOffset.x = 0;		
					break;
				}	
			textOffset.x += (charInfoWidth *_privateProperties.size/_privateProperties.fontSize * _privateProperties.spacing - _privateProperties.size) * 0.5f;
			break;	
		}

		if (_privateProperties.textAnchor == TEXT_ANCHOR.UpperLeft || _privateProperties.textAnchor == TEXT_ANCHOR.UpperRight || _privateProperties.textAnchor == TEXT_ANCHOR.UpperCenter)
		{
			textOffset.y = -heightSum/textChars.Length; 
		}
		
		else if (_privateProperties.textAnchor == TEXT_ANCHOR.MiddleCenter || _privateProperties.textAnchor == TEXT_ANCHOR.MiddleLeft || _privateProperties.textAnchor == TEXT_ANCHOR.MiddleRight)
		{
			textOffset.y = - (heightSum/textChars.Length) + _privateProperties.size*currentLineBreak*_privateProperties.lineSpacing*0.5f;
		}
		
		else if (_privateProperties.textAnchor == TEXT_ANCHOR.LowerLeft || _privateProperties.textAnchor == TEXT_ANCHOR.LowerRight || _privateProperties.textAnchor == TEXT_ANCHOR.LowerCenter)
		{
			textOffset.y = -heightSum/textChars.Length + _privateProperties.size*currentLineBreak*_privateProperties.lineSpacing;	
		}
		
		
		for (int i = 0; i<vertices.Length; i++) 
		{
			vertices[i].x += textOffset.x;
			vertices[i].y += textOffset.y;
		}
		
	}
	
	/// <summary>
	/// Sets the alignment.
	/// </summary>
	/// <param name='_pass'>
	/// _pass. The pass set what are we drawing (shadow, main, oituline up, outline down...)
	/// </param>
	void SetAlignment(int _pass)
	{
		
		//if (lineBreakCharCounter.Count <= 1) //If there is no linebreak alignment does nothing
		//	return;
		
		int vertexPassOffset = _pass*textChars.Length*4; // We have to align the outline,shadow and main.
		
		float charOffset = 0;
		
		for (int  i = 0; i<lineBreakCharCounter.Count; i++)
		{
				
				switch (_privateProperties.textAlignment)
				{
					case TEXT_ALIGNMENT.left:
							
					break;
						
					case TEXT_ALIGNMENT.right:
						charOffset = -lineBreakAccumulatedDistance[i]*_privateProperties.size/_privateProperties.fontSize * _privateProperties.spacing;
					break;
					
					case TEXT_ALIGNMENT.center:
				charOffset = -lineBreakAccumulatedDistance[i]*0.5f*_privateProperties.size/_privateProperties.fontSize * _privateProperties.spacing;
				break;
				}
				
				int firstCharVertex;
				
				if (i == 0)
					firstCharVertex = 0;
				else
					firstCharVertex = lineBreakCharCounter[i-1]*4;
				
				int lastCharVertex = lineBreakCharCounter[i]*4-1;
				
				for (int j = firstCharVertex+i*4+vertexPassOffset ; j<=lastCharVertex + i*4+vertexPassOffset; j++) // i*4 the "line break" characters are in the char array, so we have to jump them
				{
					vertices[j].x += charOffset;	
				}
		}
		
		
	}
	
	
	/// <summary>
	/// Sets the triangles for multimesh. This is used for the fill material
	/// </summary>
	void SetTrianglesForMultimesh()
	{
		int triangleMultiplier = 0;
		
		if (_privateProperties.enableOutline && _privateProperties.enableShadow)
			triangleMultiplier = 5;	
		else if (_privateProperties.enableOutline)
			triangleMultiplier = 4;	
		else if (_privateProperties.enableShadow)
			triangleMultiplier = 1;	
		
		int firstTriangleNormalText = triangleMultiplier*6*textChars.Length;
		
		int[] mainTriangleSubmesh =  new int[textChars.Length*6];
		
		int k = 0;

		Debug.Log (name + " : " + firstTriangleNormalText + " --> " + triangles.Length + "  :    " + (triangles.Length - firstTriangleNormalText) + " vs " + mainTriangleSubmesh.Length);

		var min = Mathf.Min(firstTriangleNormalText + mainTriangleSubmesh.Length, triangles.Length);

		for (int i = firstTriangleNormalText; i< min; i++)//triangles.Length
		{
			mainTriangleSubmesh[k] = triangles[i];
			k++;
		}
		
		k = 0;
		int styleTextTriangleNumber = textChars.Length*triangleMultiplier*6;
		int[] secondaryTriangleStyleText =  new int[styleTextTriangleNumber];
		for (int i = 0; i<styleTextTriangleNumber; i++)
		{
			secondaryTriangleStyleText[k] = triangles[i];
			k++;
		}
		
		//Asign meshes
		textMeshFilter.sharedMesh.subMeshCount = 2;
		textMeshFilter.sharedMesh.SetTriangles(mainTriangleSubmesh, 1);
		textMeshFilter.sharedMesh.SetTriangles(secondaryTriangleStyleText, 0);
	}
	
	
	/// <summary>
	/// Fonts the texure rebuild.
	/// </summary>
	void FontTexureRebuild(Font font)
	{
        //_privateProperties.font = font;
        //Debug.Log(333333);
        isDirty = true;
        //RefreshMeshEditor();
        //RefreshMesh(false);
    }
	
	void OnDisable()
	{
        //_privateProperties.font.textureRebuildCallback -= FontTexureRebuild;	

        //Debug.Log(5555555);
        //Font.textureRebuilt -= FontTexureRebuild;
    }
	
	/// <summary>
	/// Refreshs the mesh editor. Only used by the custom inspector
	/// </summary>
	public void RefreshMeshEditor()
	{
        //Debug.Log(333);
		DestroyImmediate(textMesh);
		textMesh = new Mesh{ name = GetInstanceID().ToString() };  //We have to recreate the mesh to avoid problems with mesh references when duplicating objects
		MeshFilter textMeshFilter = GetComponent<MeshFilter>();

        CacheTextVars();

        if (textMeshFilter != null)
		{
			textMeshFilter.sharedMesh = textMesh;
			if (GetComponent<Renderer>().sharedMaterial == null)
				GetComponent<Renderer>().sharedMaterial = _privateProperties.font.material;

            RefreshMesh(true);
        }
	}
	
	
	public int GetVertexCount()
	{
		if (vertices != null)
			return vertices.Length;
		else
			return 0;
	}

    void Update()
    {
        //#if UNITY_EDITOR
        //        if (!Application.isPlaying && GUIChanged == false)
        //            return;
        //#endif

        if (isDirty)
        {
            isDirty = false;
            //LocalisedString.UpdateString();
            //RefreshMeshEditor();
            RefreshMesh(true);
        }
        //RefreshMesh(true);
    }


    /// <summary>
    /// Sets the color hidden. This will not change the values in inspector, but its more efficent for changing vertex colors
    /// </summary>
    /// <param name='_topColor'>
    /// _top color.
    /// </param>
    void SetColor(Color _topColor, Color _bottomColor)
	{
#if UNITY_EDITOR
        if (!Application.isPlaying && GUIChanged == false)
            return;
#endif

        if (colors == null || textMesh == null)
			return;
		
		int initialVertex = GetInitialVertexToColorize(TEXT_COMPONENT.Main);
		
		int j = 0;	
		for (int i = initialVertex; i<GetFinalVertexToColorize(TEXT_COMPONENT.Main); i ++ )
		{
			if (j  == 0 || j == 1)
				colors[i] = _bottomColor; 	
			
			else
				colors[i] = _topColor;
			
			j++;
			
			if (j > 3)
				j = 0;
		}
		
		textMesh.colors = colors; 
	}
	
	/// <summary>
	/// Sets the top and bottom color at the same time
	/// </summary>
	/// <param name='_color'>
	/// _color.
	/// </param>
	public void SetColor(Color _color)
	{
		if (colors == null || textMesh == null)
			return;
		
		int initalVertex = GetInitialVertexToColorize(TEXT_COMPONENT.Main);
		
		for (int i = initalVertex; i<GetFinalVertexToColorize(TEXT_COMPONENT.Main); i ++ )
		{
			colors[i] = _color;			
		}	
		
		textMesh.colors = colors; 
	}
	
	
	/// <summary>
	/// Sets the shadow's color
	/// </summary>
	/// <param name='_color'>
	/// _color.
	/// </param>
	void SetShadowColor(Color _color)
	{
		//#if UNITY_EDITOR	
		//if (!Application.isPlaying && GUIChanged == false)
		//	return;
		//#endif
		
		if (colors == null || textMesh == null )
			return;
		
		int initalVertex = GetInitialVertexToColorize(TEXT_COMPONENT.Shadow);
		
		for (int i = initalVertex; i<GetFinalVertexToColorize(TEXT_COMPONENT.Shadow); i ++ )
		{
			colors[i] = _color;			
		}	
		
		textMesh.colors = colors; 	
	}
	
	/// <summary>
	/// Sets the outline colour
	/// </summary>
	/// <param name='_color'>
	/// _color.
	/// </param>
	void SetOutlineColor(Color _color)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying && GUIChanged == false)
            return;
#endif

        if (colors == null || textMesh == null)
			return;
		
		int initalVertex = GetInitialVertexToColorize(TEXT_COMPONENT.Outline);
		
		for (int i = initalVertex; i<GetFinalVertexToColorize(TEXT_COMPONENT.Outline); i ++ )
		{
			colors[i] = _color;			
		}	
		
		textMesh.colors = colors; 	
	}
	
	/// <summary>
	/// Gets the initial vertex to colorize.
	/// </summary>
	/// <returns>
	/// The initial vertex to colorize.
	/// </returns>
	/// <param name='_textComponent'>
	/// _text component.
	/// </param>
	private int GetInitialVertexToColorize(TEXT_COMPONENT _textComponent)
	{
		if (textChars == null)
				textChars = _privateProperties.text.ToCharArray();
		
		int meshOptionMultipler = 0;
		
		switch (_textComponent)
		{
			case TEXT_COMPONENT.Main:
			
				if (_privateProperties.enableShadow && _privateProperties.enableOutline)
					meshOptionMultipler = 5;
				else if (_privateProperties.enableOutline)
					meshOptionMultipler = 4;
				else if (_privateProperties.enableShadow)
					meshOptionMultipler = 1;
			break;
			case TEXT_COMPONENT.Shadow:
					meshOptionMultipler = 0;
			break;
			
			case TEXT_COMPONENT.Outline:
				if (_privateProperties.enableShadow)	
					meshOptionMultipler = 1;
				else
					meshOptionMultipler = 0;
			break;
		}
		
		return  textChars.Length*4*meshOptionMultipler;	
	}
	
	private int GetFinalVertexToColorize(TEXT_COMPONENT _textComponent)
	{
		
		if (textChars == null)
			textChars = _privateProperties.text.ToCharArray();
		    
		int lastVertex = 0;
		int meshOptionMultipler = 0;
		
		switch (_textComponent)
		{
			case TEXT_COMPONENT.Main:
				
				if (_privateProperties.enableShadow && _privateProperties.enableOutline)
						meshOptionMultipler = 6;
				else if (_privateProperties.enableOutline)
						meshOptionMultipler = 5;
				else if (_privateProperties.enableShadow)
						meshOptionMultipler = 2;
				
				lastVertex = textChars.Length*4*meshOptionMultipler;
			
			break;
			
			case TEXT_COMPONENT.Shadow:
				lastVertex =  textChars.Length*4;
			break;
			
			case TEXT_COMPONENT.Outline:
				
				if (_privateProperties.enableShadow)
					meshOptionMultipler = 1;
				else
					meshOptionMultipler = 0;
				
				lastVertex = textChars.Length*4*(meshOptionMultipler +4);
			break;
		}
		
		return lastVertex;
			
	}
	
	void ChangeFont()
	{
#if UNITY_EDITOR
        if (!Application.isPlaying && GUIChanged == false)
            return;
#endif
        if (!dontOverrideMaterials && _privateProperties.customFillMaterial == null)
		{
			//Material currentMaterial 		= new Material(textRenderer.sharedMaterial);
			//currentMaterial.mainTexture 	= _privateProperties.font.material.mainTexture;
			//textRenderer.sharedMaterial 	= currentMaterial;
			textRenderer.sharedMaterial = _privateProperties.font.material;
            isDirty = true;
            //Debug.Log(2222);
		}
		
	}
	
	
		
		
}
