using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {
	// global
	public enum Player { Player1, Player2, Empty, Draw };
	private Player curr_player;
	private Player winner;
	private int cnt;
	private int cnt_d;
	private int cnt_rd;
	private int[] cnt_row;
	private int[] cnt_col;

	// grid
	private float grid_w; 
	private float grid_h;
	private float grid_x;
	private float grid_y;
	private Rect[,] grid_rect;
	private Player[,] grid_text;

	// title
	private float title_w;
	private float title_h;
	private float title_x;
	private float title_y;
	private Rect title_rect;
	private string title_text;
	private GUIStyle title_style;

	// reset
	private float reset_w;
	private float reset_h;
	private float reset_x;
	private float reset_y;
	private Rect reset_rect;
	private string reset_text;

	// result
	private float result_w;
	private float result_h;
	private float result_x;
	private float result_y;
	private Rect result_rect;
	private string result_text;
	private GUIStyle result_style;

	// Use this for initialization
	private void Start () {
		// grid
		grid_w = 100;
		grid_h = 100;
		grid_x = 0.5f * Screen.width - 1.5f * grid_w;
		grid_y = 0.5f * Screen.height - 1.5f * grid_h;
		grid_rect = new Rect[3,3];
		grid_text = new Player[3,3];

		// title
		title_w = 5.0f * grid_w;
		title_h = grid_h;
		title_x = grid_x - 0.5f * (title_w - 3.0f * grid_w);
		title_y = grid_y - grid_h;
		title_rect = new Rect(title_x, title_y, title_w, title_h);
		title_text = "Tic Tac Toe";
		title_style = new GUIStyle {
			fontSize = 50,
			fontStyle = FontStyle.Bold,
			alignment = TextAnchor.MiddleCenter
		};

		// reset
		reset_w = 1.8f * grid_w;
		reset_h = 0.6f * grid_h;
		reset_x = grid_x + 1.0f * grid_w - 0.5f * (reset_w - grid_w);
		reset_y = grid_y + 3.0f * grid_h - 0.5f * (reset_h - grid_h);
		reset_rect = new Rect(reset_x, reset_y, reset_w, reset_h);
		reset_text = "Reset";

		// result
		result_w = 5.0f * grid_w;
		result_h = grid_h;
		result_x = grid_x - 0.5f * (result_w - 3.0f * grid_w);
		result_y = grid_y + grid_h;
		result_rect = new Rect(result_x, result_y, result_w, result_h);
		result_style = new GUIStyle {
			fontSize = 75,
			fontStyle = FontStyle.Bold,
			alignment = TextAnchor.MiddleCenter
		};
		result_style.normal.textColor = Color.red;

		// reset
		Reset();
	}

	private void OnGUI() {
		// grid
		for (int i = 0; i < 3; ++i) {
			for (int j = 0; j < 3; ++j) {
				string text;
				switch (grid_text[i,j]) {
					case Player.Player1:
						text = "X";
						break;
					case Player.Player2:
						text = "O";
						break;
					default:
						text = "";
						break;
				};

				if (GUI.Button(grid_rect[i,j], text) && grid_text[i,j] == Player.Empty && winner == Player.Empty) {
					// winner
					++cnt;
					if (cnt == 9) winner = Player.Draw;
					if (curr_player == Player.Player1) {
						if (i == j) ++cnt_d;
						if (i+j == 2) ++cnt_rd;
						++cnt_row[i];
						++cnt_col[j];
					} else {
						if (i == j) --cnt_d;
						if (i+j == 2) --cnt_rd;
						--cnt_row[i];
						--cnt_col[j];
					}
					if (cnt_d == 3 || cnt_rd == 3) {
						winner = Player.Player1;
					}
					if (cnt_d == -3 || cnt_rd == -3) {
						winner = Player.Player2;
					}
					for (int k = 0; k < 3; ++k) {
						if (cnt_row[k] == 3) winner = Player.Player1;
						if (cnt_row[k] == -3) winner = Player.Player2;
						if (cnt_col[k] == 3) winner = Player.Player1;
						if (cnt_col[k] == -3) winner = Player.Player2;
					}

					// current player
					grid_text[i,j] = curr_player;
					if (curr_player == Player.Player1) {
						curr_player = Player.Player2;
					} else {
						curr_player = Player.Player1;
					}
				}
			}
		}

		// title
		GUI.Label(title_rect, title_text, title_style);

		// reset
		if (GUI.Button(reset_rect, reset_text)) {
			Reset();
		};

		// result
		if (winner != Player.Empty) {
			string text;
			switch (winner) {
				case Player.Player1:
					text = "Player1 Win!";
					break;
				case Player.Player2:
					text = "Player2 Win!";
					break;
				default:
					text = "You are all smart!";
					break;
			}
			GUI.Label(result_rect, text, result_style);
		}
	}

	private void Reset() {
		// player
		curr_player = Player.Player1;
		winner = Player.Empty;

		// count
		cnt = 0;
		cnt_d = 0;
		cnt_rd = 0;
		cnt_row = new int[3];
		cnt_col = new int[3];
		for (int i = 0; i < 3; ++i) {
			cnt_row[i] = 0;
			cnt_col[i] = 0;
		}

		// grid
		for (int i = 0; i < 3; ++i) {
			for (int j = 0; j < 3; ++j) {
				grid_rect[i,j] = new Rect(grid_x + j*grid_w, grid_y + i*grid_h, grid_w, grid_h);
				grid_text[i,j] = Player.Empty;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
