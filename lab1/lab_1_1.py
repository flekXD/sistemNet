import msvcrt
import time

def read_numbers_from_file(file_name):
    with open(file_name, 'r') as file:
        numbers = [int(line.strip()) for line in file]
    return numbers

def bubble_sort(numbers):
    n = len(numbers)
    for i in range(n-1):
        for j in range(0, n-i-1):
            if numbers[j] > numbers[j+1]:
                numbers[j], numbers[j+1] = numbers[j+1], numbers[j]
                print(numbers)  # Показуємо кроки сортування
                time.sleep(1)   # Затримка на 1 секунду для наочності

def main():
    file_name = 'data.dat'
    numbers = read_numbers_from_file(file_name)

    print("Натисніть пробіл для сортування:")
    while True:
        if msvcrt.kbhit():
            key = msvcrt.getch()
            if key == b' ':
                print("Сортування почалось...")
                bubble_sort(numbers)
                print("Робота завершена.")
                break

if __name__ == "__main__":
    main()
