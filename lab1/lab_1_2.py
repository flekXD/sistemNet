import tkinter as tk
import mmap
import time

def display_data(window, mem_file, array_size):
    data = mem_file.read(array_size * 2)  # читаємо дані з файлу у пам'ять
    numbers = [int.from_bytes(data[i:i+2], byteorder='little') for i in range(0, len(data), 2)]

    window.geometry(f"{array_size * 20}x200")  # змінюємо розмір вікна залежно від кількості чисел у масиві

    canvas = tk.Canvas(window)
    canvas.pack(expand=tk.YES, fill=tk.BOTH)

    for i, number in enumerate(numbers):
        canvas.create_text(i * 20 + 10, 100, text="*", font=("Arial", number), anchor=tk.CENTER)

def main():
    file_name = 'data.dat'
    array_size = 30

    root = tk.Tk()
    root.title("Display Data")

    with open(file_name, 'r+b') as file:
        mem_file = mmap.mmap(file.fileno(), 0)  # проектуємо файл у пам'ять

        def update_display():
            display_data(root, mem_file, array_size)
            root.after(500, update_display)  # встановлюємо таймер на 0.5 секунди для оновлення вікна

        update_display()

        root.mainloop()

if __name__ == "__main__":
    main()
