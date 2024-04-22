import pygame
import os
import tkinter as tk

class SoundExample:
    def __init__(self, root):
        self.root = root
        self.root.title("Sound Player")

        # Initialize Pygame and the sound player
        pygame.init()
        self.soundPlayer = None

        # Create a button
        play_button = tk.Button(root, text="Play Sound", command=self.play_sound)
        play_button.pack()

    def play_sound(self):
        if self.soundPlayer is None:
            # input sound file name here
            sound_file = "C:\\Users\\darude_rig\\Desktop\\dayz sounboard\\media\\sounds\\AKM.wav"
            sound_file_path = os.path.abspath(sound_file)
            if os.path.exists(sound_file_path):
                pygame.mixer.init() # currently default settings can change frequency and channels
                self.soundPlayer = pygame.mixer.Sound(sound_file_path)
                self.soundPlayer.play(-1)  # Play the sound indefinitely

    def stop_sound(self):
        if self.soundPlayer is not None:
            self.soundPlayer.stop()

if __name__ == "__main__":
    root = tk.Tk()
    sound = SoundExample(root)
    root.mainloop()

