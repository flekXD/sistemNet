import random

def generate_random_numbers(file_name, count, min_value, max_value):
    with open(file_name, 'w') as file:
        for _ in range(count):
            random_number = random.randint(min_value, max_value)
            file.write(str(random_number) + '\n')

def main():
    file_name = 'data.dat'
    count = random.randint(20, 30)
    min_value = 10
    max_value = 100

    generate_random_numbers(file_name, count, min_value, max_value)
    print(f"Файл {file_name} згенеровано з {count} випадковими числами в діапазоні від {min_value} до {max_value}.")

if __name__ == "__main__":
    main()
