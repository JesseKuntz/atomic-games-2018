namespace ai
{
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

    public static class AI
    {
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
                            if (k >= 0 && gameMessage.board[k][j] == 0 && !moves.Contains(new KeyValuePair<int, int>(k, j))) moves.Add(new KeyValuePair<int, int>(k, j));
                        }

                        // UP RIGHT
                        if (i-1 >= 0 && j+1 <= 7 && gameMessage.board[i-1][j+1] == otherPlayer) {
                            int k = i-2;
                            int l = j+2;
                            while(k >= 0 && l <= 7 && gameMessage.board[k][l] == otherPlayer) {
                                k--;
                                l++;
                            }
                            if (k >= 0 && l <= 7 && gameMessage.board[k][l] == 0 && !moves.Contains(new KeyValuePair<int, int>(k, l))) moves.Add(new KeyValuePair<int, int>(k, l));
                        }

                        // RIGHT
                        if (j+1 <= 7 && gameMessage.board[i][j+1] == otherPlayer) {
                            int l = j+2;
                            while(l <= 7 && gameMessage.board[i][l] == otherPlayer) {
                                l++;
                            }
                            if (l <= 7 && gameMessage.board[i][l] == 0 && !moves.Contains(new KeyValuePair<int, int>(i, l))) moves.Add(new KeyValuePair<int, int>(i, l));
                        }

                        // DOWN RIGHT
                        if (i+1 <= 7 && j+1 <= 7 && gameMessage.board[i+1][j+1] == otherPlayer) {
                            int k = i+2;
                            int l = j+2;
                            while(k <= 7 && l <= 7 && gameMessage.board[k][l] == otherPlayer) {
                                k++;
                                l++;
                            }
                            if (k <= 7 && l <= 7 && gameMessage.board[k][l] == 0 && !moves.Contains(new KeyValuePair<int, int>(k, l))) moves.Add(new KeyValuePair<int, int>(k, l));
                        }

                        // DOWN
                        if (i+1 <= 7 && gameMessage.board[i+1][j] == otherPlayer) {
                            int k = i+2;
                            while(k <= 7 && gameMessage.board[k][j] == otherPlayer) {
                                k++;
                            }
                            if (k <= 7 && gameMessage.board[k][j] == 0 && !moves.Contains(new KeyValuePair<int, int>(k, j))) moves.Add(new KeyValuePair<int, int>(k, j));
                        }

                        // DOWN LEFT
                        if (i+1 <= 7 && j-1 >= 0 && gameMessage.board[i+1][j-1] == otherPlayer) {
                            int k = i+2;
                            int l = j-2;
                            while(k <= 7 && l >= 0 && gameMessage.board[k][l] == otherPlayer) {
                                k++;
                                l--;
                            }
                            if (k <= 7 && l >= 0 && gameMessage.board[k][l] == 0 && !moves.Contains(new KeyValuePair<int, int>(k, l))) moves.Add(new KeyValuePair<int, int>(k, l));
                        }

                        // LEFT
                        if (j-1 >= 0 && gameMessage.board[i][j-1] == otherPlayer) {
                            int l = j-2;
                            while(l >= 0 && gameMessage.board[i][l] == otherPlayer) {
                                l--;
                            }
                            if (l >= 0 && gameMessage.board[i][l] == 0 && !moves.Contains(new KeyValuePair<int, int>(i, l))) moves.Add(new KeyValuePair<int, int>(i, l));
                        }

                        // UP LEFT
                        if (i-1 >= 0 && j-1 >= 0 && gameMessage.board[i-1][j-1] == otherPlayer) {
                            int k = i-2;
                            int l = j-2;
                            while(k >= 0 && l >= 0 && gameMessage.board[k][l] == otherPlayer) {
                                k--;
                                l--;
                            }
                            if (k >= 0 && l >= 0 && gameMessage.board[k][l] == 0 && !moves.Contains(new KeyValuePair<int, int>(k, l))) moves.Add(new KeyValuePair<int, int>(k, l));
                        }
                    }
                }
            }
            return moves;
        }

        public static int numberOfChips(GameMessage gameMessage) {
            int total = 0;
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (gameMessage.board[i][j] == gameMessage.player) total++;
                }
            }
            return total;
        }

        public static int evaluate(GameMessage gameMessage) {
            int score = 0;

            score = numberOfChips(gameMessage);

            return score;
        }

        public static int[][] makeMove(GameMessage gameMessage, KeyValuePair<int, int> move) {
            int otherPlayer;
            if (gameMessage.player == 1) otherPlayer = 2;
            else otherPlayer = 1;

            int i = move.Key;
            int j = move.Value;

            // UP
            if (i-1 >= 0 && gameMessage.board[i-1][j] == otherPlayer) {
                int k = i-2;
                while(k >= 0 && gameMessage.board[k][j] == otherPlayer) {
                    k--;
                }
                if (k >= 0 && gameMessage.board[k][j] == gameMessage.player) {
                    k = i-1;
                    while(k >= 0 && gameMessage.board[k][j] == otherPlayer) {
                        gameMessage.board[k][j] = gameMessage.player;
                        k--;
                    }
                };
            }

            // RIGHT UP
            if (i-1 >= 0 && j+1 <= 7 && gameMessage.board[i-1][j+1] == otherPlayer) {
                int k = i-2;
                int l = j+2;
                while(k >= 0 && l <= 7 && gameMessage.board[k][l] == otherPlayer) {
                    k--;
                    l++;
                }
                if (k >= 0  && l <= 7 && gameMessage.board[k][l] == gameMessage.player) {
                    k = i-1;
                    l = j+1;
                    while(k >= 0 && l <= 7 && gameMessage.board[k][l] == otherPlayer) {
                        gameMessage.board[k][l] = gameMessage.player;
                        k--;
                        l++;
                    }
                };
            }

            // Right
            if (j+1 <= 7 && gameMessage.board[i][j+1] == otherPlayer) {
                int l = j+2;
                while(l <= 7 && gameMessage.board[i][l] == otherPlayer) {
                    l++;
                }
                if (l <= 7 && gameMessage.board[i][l] == gameMessage.player) {
                    l = j+1;
                    while(l <= 7 && gameMessage.board[i][l] == otherPlayer) {
                        gameMessage.board[i][l] = gameMessage.player;
                        l++;
                    }
                };
            }

            // RIGHT DOWN
            if (i+1 <= 7 && j+1 <= 7 && gameMessage.board[i+1][j+1] == otherPlayer) {
                int k = i+2;
                int l = j+2;
                while(k <= 7 && l <= 7 && gameMessage.board[k][l] == otherPlayer) {
                    k++;
                    l++;
                }
                if (k <= 7  && l <= 7 && gameMessage.board[k][l] == gameMessage.player) {
                    k = i+1;
                    l = j+1;
                    while(k <= 7 && l <= 7 && gameMessage.board[k][l] == otherPlayer) {
                        gameMessage.board[k][l] = gameMessage.player;
                        k++;
                        l++;
                    }
                };
            }

            // DOWN
            if (i+1 <= 7 && gameMessage.board[i+1][j] == otherPlayer) {
                int k = i+2;
                while(k <= 7 && gameMessage.board[k][j] == otherPlayer) {
                    k++;
                }
                if (k <= 7 && gameMessage.board[k][j] == gameMessage.player) {
                    k = i+1;
                    while(k >= 7 && gameMessage.board[k][j] == otherPlayer) {
                        gameMessage.board[k][j] = gameMessage.player;
                        k++;
                    }
                };
            }

            // LEFT DOWN
            if (i+1 <= 7 && j-1 >= 0 && gameMessage.board[i+1][j-1] == otherPlayer) {
                int k = i+2;
                int l = j-2;
                while(k <= 7 && l >= 0 && gameMessage.board[k][l] == otherPlayer) {
                    k++;
                    l--;
                }
                if (k <= 7  && l >= 0 && gameMessage.board[k][l] == gameMessage.player) {
                    k = i+1;
                    l = j-1;
                    while(k <= 7 && l >= 0 && gameMessage.board[k][l] == otherPlayer) {
                        gameMessage.board[k][l] = gameMessage.player;
                        k++;
                        l--;
                    }
                };
            }

            // LEFT
            if (j-1 >= 0 && gameMessage.board[i][j-1] == otherPlayer) {
                int l = j-2;
                while(l >= 0 && gameMessage.board[i][l] == otherPlayer) {
                    l--;
                }
                if (l >= 0 && gameMessage.board[i][l] == gameMessage.player) {
                    l = j-1;
                    while(l >= 0 && gameMessage.board[i][l] == otherPlayer) {
                        gameMessage.board[i][l] = gameMessage.player;
                        l--;
                    }
                };
            }

            // LEFT UP
            if (i-1 >= 0 && j-1 >= 0 && gameMessage.board[i+1][j-1] == otherPlayer) {
                int k = i-2;
                int l = j-2;
                while(k >= 0 && l >= 0 && gameMessage.board[k][l] == otherPlayer) {
                    k--;
                    l--;
                }
                if (k >= 0  && l >= 0 && gameMessage.board[k][l] == gameMessage.player) {
                    k = i-1;
                    l = j-1;
                    while(k >= 0 && l >= 0 && gameMessage.board[k][l] == otherPlayer) {
                        gameMessage.board[k][l] = gameMessage.player;
                        k--;
                        l--;
                    }
                };
            }

            gameMessage.board[i][j] = gameMessage.player;

            return gameMessage.board;
        }

        // public static MoveResult minimax(GameMessage gameMessage, Stopwatch w) {
        //     // Check if we are out of time
        //     if (w.ElapsedMilliseconds > gameMessage.maxTurnTime) {
        //         // throw exception?
        //     }

        //     // Base Case (CHANGE)
        //     if (true) {

        //     }

        //     List<KeyValuePair<int, int>> moves = listOfMoves(gameMessage);

        //     if(gameMessage.player == 1) {
        //         int maxScore = int.MinValue;
        //         foreach (KeyValuePair<int, int> move in moves) {
        //             GameMessage gmCopy = new GameMessage(gameMessage);
        //             gmCopy.board = makeMove(gmCopy.board, move);

        //         }
        //     }
        // }

        public static int[] NextMove(GameMessage gameMessage)
        {
            var nextMove = new[] {1, 1};
            return nextMove;
        }

    }

    class MoveResult
    {
        private KeyValuePair<int, int> bestMove;
        private int bestScore;
        private bool endGame;
        public MoveResult(KeyValuePair<int, int> move, int score, bool end)
        {
            bestMove = move;
            bestScore = score;
            endGame = end;
        }

        public KeyValuePair<int, int> getMove() { return bestMove; }

        public int getScore() { return bestScore; }

        public bool isEndGame() { return endGame; }
    }
}
