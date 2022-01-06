using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Lottery.Source.ViewModel.Base;

namespace Lottery.Source.ViewModel {
    public class MainWindowVM : BaseViewModel {
        private readonly Random random = new Random();

        private string _buttonText, _iconImage, _output0, _output1, _output2, _output3, _output4, _output5, _output6, _debugSimulationLoopAmount, _debugStatsOutput,
            _debugRowHeight, _debugVisibility, _colorBorder;

        private bool canSimulate = true;

        public MainWindowVM() {
            IconImage = "../../Resources/Images/icon.png";
            ButtonText = "Simulate Lottery";
            DebugVisibility = "HIDDEN";
            DebugRowHeight = "0";
            ColorBorder = "White";
            DebugSimulationLoopAmount = "";
        }

        public ICommand ButtonCommand => new DelegateCommand(ButtonLogic, true);

        public ICommand GlobalKeyboardListener => new DelegateCommand(GlobalKeyboardListenerLogic, true);

        private void ButtonLogic(object param) {
            switch (param) {
            case "lottery":
                if (canSimulate) {
                    canSimulate = false;
                    ButtonText = "Done:  " + DateTime.Now;
                    int[] output = RunSimulation();
                    Output0 = output[0].ToString();
                    Output1 = output[1].ToString();
                    Output2 = output[2].ToString();
                    Output3 = output[3].ToString();
                    Output4 = output[4].ToString();
                    Output5 = output[5].ToString();

                    switch (output[6]) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        Output6 = "/";
                        break;
                    case 4:
                    case 5:
                    case 6:
                        Output6 = "2x";
                        break;
                    case 7:
                    case 8:
                        Output6 = "3x";
                        break;
                    case 9:
                        Output6 = "4x";
                        break;
                    }
                }

                break;
            case "stats":
                try {
                    int loopAmount = Convert.ToInt32(DebugSimulationLoopAmount);
                    int[,] column_number = new int[7, 10];

                    for (int i = 0; i < loopAmount; i++) {
                        int[] simulation = RunSimulation();
                        for (int j = 0; j < simulation.Length; j++) {
                            foreach (int num in simulation) {
                                column_number[j, num]++;
                            }
                        }
                    }

                    DebugStatsOutput = "Column 1:\n0: " + column_number[0, 0] + ",  1: " + column_number[0, 1] + ",  2: " + column_number[0, 2] + ",  3: " + column_number[0, 3] +
                                       ",  4: " + column_number[0, 4] + ",  5: " + column_number[0, 5] + ",  6: " + column_number[0, 6] + ",  7: " + column_number[0, 7] +
                                       ",  8: " + column_number[0, 8] + ",  9: " + column_number[0, 9];
                } catch (Exception e) {
                    DebugStatsOutput = e.Message;
                }

                break;
            }
        }

        private int[] RunSimulation() {
            int[] output = new int[7];
            output[0] = random.Next(0, 10);
            output[1] = random.Next(0, 10);
            output[2] = random.Next(0, 10);
            output[3] = random.Next(0, 10);
            output[4] = random.Next(0, 10);
            output[5] = random.Next(0, 10);
            output[6] = random.Next(0, 10);
            return output;
        }

        private string VerifyInput(string value) {
            value = Regex.Replace(value, @"[^0-9]+", "");
            return value;
        }

        private void GlobalKeyboardListenerLogic(object obj) {
            if (obj.Equals("F12")) {
                if (DebugVisibility == "VISIBLE") {
                    DebugVisibility = "HIDDEN";
                    DebugRowHeight = "0";
                } else {
                    DebugVisibility = "VISIBLE";
                    DebugRowHeight = "*";
                }
            }
        }

        #region Fields

        public string IconImage {
            get => _iconImage;
            set {
                _iconImage = value;
                RaisePropertyChangedEvent("IconImage");
            }
        }

        public string ColorBorder {
            get => _colorBorder;
            set {
                _colorBorder = value;
                RaisePropertyChangedEvent("ColorBorder");
            }
        }

        public string Output0 {
            get => _output0;
            set {
                _output0 = value;
                RaisePropertyChangedEvent("Output0");
            }
        }

        public string Output1 {
            get => _output1;
            set {
                _output1 = value;
                RaisePropertyChangedEvent("Output1");
            }
        }

        public string Output2 {
            get => _output2;
            set {
                _output2 = value;
                RaisePropertyChangedEvent("Output2");
            }
        }

        public string Output3 {
            get => _output3;
            set {
                _output3 = value;
                RaisePropertyChangedEvent("Output3");
            }
        }

        public string Output4 {
            get => _output4;
            set {
                _output4 = value;
                RaisePropertyChangedEvent("Output4");
            }
        }

        public string Output5 {
            get => _output5;
            set {
                _output5 = value;
                RaisePropertyChangedEvent("Output5");
            }
        }

        public string Output6 {
            get => _output6;
            set {
                _output6 = value;
                RaisePropertyChangedEvent("Output6");
            }
        }

        public string DebugSimulationLoopAmount {
            get => _debugSimulationLoopAmount;
            set {
                _debugSimulationLoopAmount = VerifyInput(value);
                RaisePropertyChangedEvent("DebugSimulationLoopAmount");
            }
        }

        public string DebugStatsOutput {
            get => _debugStatsOutput;
            set {
                _debugStatsOutput = value;
                RaisePropertyChangedEvent("DebugStatsOutput");
            }
        }

        public string DebugRowHeight {
            get => _debugRowHeight;
            set {
                _debugRowHeight = value;
                RaisePropertyChangedEvent("DebugRowHeight");
            }
        }

        public string DebugVisibility {
            get => _debugVisibility;
            set {
                _debugVisibility = value;
                RaisePropertyChangedEvent("DebugVisibility");
            }
        }

        public string ButtonText {
            get => _buttonText;
            set {
                _buttonText = value;
                RaisePropertyChangedEvent("ButtonText");
            }
        }

        #endregion
    }
}