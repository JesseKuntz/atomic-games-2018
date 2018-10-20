namespace ai
{
using System;
using System.Collections.Generic;
    public static class AI
    {
        // public static KeyValuePair<int, int> findOpenSpace(int direction) {
        //     if ()
        // }
        public static List<KeyValuePair<int, int>> listOfMoves(GameMessage gameMessage) {
            int otherPlayer;
            if (gameMessage.player == 1) otherPlayer = 2;
            else otherPlayer = 1;

            List<KeyValuePair<int, int>> moves = new List<KeyValuePair<int, int>>();

            for(int i = 0; i < 8; i++) {
                for(int j = 0; j < 8; j++) {
                    if (gameMessage.board[i][j] == gameMessage.player) {
                        // UP
                        if (i-1 >= 0 && gameMessage.board[i-1][j] == otherPlayer) {
                            int k = i-2;
                            while(k >= 0 && gameMessage.board[k][j] == otherPlayer) {
                                k--;
                            }
                            if (k >= 0 && gameMessage.board[k][j] == 0) moves.Add(new KeyValuePair<int, int>(k, j));
                        }

                        // UP RIGHT
                        if (i-1 >= 0 && j+1 <= 7 && gameMessage.board[i-1][j+1] == otherPlayer) {
                            int k = i-2;
                            int l = j+2;
                            while(k >= 0 && l <= 7 && gameMessage.board[k][l] == otherPlayer) {
                                k--;
                                l++;
                            }
                            if (k >= 0 && l <= 7 && gameMessage.board[k][l] == 0) moves.Add(new KeyValuePair<int, int>(k, l));
                        }

                        // RIGHT
                        if (j+1 <= 7 && gameMessage.board[i][j+1] == otherPlayer) {
                            int l = j+2;
                            while(l <= 7 && gameMessage.board[i][l] == otherPlayer) {
                                l++;
                            }
                            if (l <= 7 && gameMessage.board[i][l] == 0) moves.Add(new KeyValuePair<int, int>(i, l));
                        }

                        // DOWN RIGHT
                        if (i+1 <= 7 && j+1 <= 7 && gameMessage.board[i+1][j+1] == otherPlayer) {
                            int k = i+2;
                            int l = j+2;
                            while(k <= 7 && l <= 7 && gameMessage.board[k][l] == otherPlayer) {
                                k++;
                                l++;
                            }
                            if (k <= 7 && l <= 7 && gameMessage.board[k][l] == 0) moves.Add(new KeyValuePair<int, int>(k, l));
                        }

                        // DOWN
                        if (i+1 <= 7 && gameMessage.board[i+1][j] == otherPlayer) {
                            int k = i+2;
                            while(k <= 7 && gameMessage.board[k][j] == otherPlayer) {
                                k++;
                            }
                            if (k <= 7 && gameMessage.board[k][j] == 0) moves.Add(new KeyValuePair<int, int>(k, j));
                        }

                        // DOWN LEFT
                        if (i+1 <= 7 && j-1 >= 0 && gameMessage.board[i+1][j-1] == otherPlayer) {
                            int k = i+2;
                            int l = j-2;
                            while(k <= 7 && l >= 0 && gameMessage.board[k][l] == otherPlayer) {
                                k++;
                                l--;
                            }
                            if (k <= 7 && l >= 0 && gameMessage.board[k][l] == 0) moves.Add(new KeyValuePair<int, int>(k, l));
                        }

                        // LEFT
                        if (j-1 >= 0 && gameMessage.board[i][j-1] == otherPlayer) {
                            int l = j-2;
                            while(l >= 0 && gameMessage.board[i][l] == otherPlayer) {
                                l--;
                            }
                            if (l >= 0 && gameMessage.board[i][l] == 0) moves.Add(new KeyValuePair<int, int>(i, l));
                        }

                        // UP LEFT
                        if (i-1 >= 0 && j-1 >= 0 && gameMessage.board[i-1][j-1] == otherPlayer) {
                            int k = i-2;
                            int l = j-2;
                            while(k >= 0 && l >= 0 && gameMessage.board[k][l] == otherPlayer) {
                                k--;
                                l--;
                            }
                            if (k >= 0 && l >= 0 && gameMessage.board[k][l] == 0) moves.Add(new KeyValuePair<int, int>(k, l));
                        }
                    }
                }
            }
            return moves;
        }
        public static int[] NextMove(GameMessage gameMessage)
        {
            var nextMove = new[] {1, 1};
            return nextMove;
        }

    }
}
