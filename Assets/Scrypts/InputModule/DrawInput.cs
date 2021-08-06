﻿using Assets.Scrypts.LevelManagerSystem;
using PDollarGestureRecognizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scrypts.InputModule
{
    class DrawInput : InputBehaviour, IEndDragHandler, IDragHandler
	{
		public UnityEvent OnErrorInput;
		[SerializeField] LineRenderer currentGestureLineRenderer;

		private List<Gesture> trainingSet = new List<Gesture>();
		private List<Point> points = new List<Point>();

		private int vertexCount;
		private string symbol;

		private void Start()
		{
			//Load pre-made gestures
			string[] dirnames = LevelData.levelData.symbols;
			foreach (string dirname in dirnames)
			{
				Debug.Log(dirname);
				TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("Letters/" + dirname);
				foreach (TextAsset gestureXml in gesturesXml)
					trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
			}
		}
		protected override string InputSymbol()
		{
			return symbol;
		}

        public void OnDrag(PointerEventData eventData)
		{
			Vector2 position = Camera.main.ScreenToWorldPoint(eventData.position);
			points.Add(new Point(position.x, -position.y, 0));

			currentGestureLineRenderer.positionCount = ++vertexCount;
			currentGestureLineRenderer.SetPosition(vertexCount - 1, new Vector3(position.x, position.y, 10));
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			Gesture candidate = new Gesture(points.ToArray());
			Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
			Debug.Log(gestureResult.Score);
			if (gestureResult.Score > 0.8f)
			{
				symbol = gestureResult.GestureClass;
				Debug.Log($"Bukva: {symbol} with {gestureResult.Score}");
				OnSymbolInput();
			}
			else
				OnErrorInput.Invoke();
			points.Clear();
			vertexCount = 0;
			currentGestureLineRenderer.positionCount = 0;
		}
    }
}
