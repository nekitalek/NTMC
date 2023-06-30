import readchar
import os
import math
import random


def f(num, test_count):
    print("тест Ферма")
    testers = []
    for i in range(test_count):
        rnd = random.randint(1, num - 1)
        # print( % num != 1)
        # print(i)
        testers.append(rnd)
        if pow(rnd, (num - 1), num) != 1:
            print("Нарушено условие малой теоремы ферма")
            print("Число составное. Основание с нарушением:\t", rnd)
            return
    print("Число, вероятно, простое. Был проверен массив оснований:")
    print(testers)
    return


def rm(num, test_count):
    print("тест Рабина Миллера")
    testers = []
    num_dec = num-1
    step = 0
    iter = 0
    for iter in range(test_count):
        while num_dec % 2 == 0:
            num_dec = num_dec >> 1
            step = step + 1
        rnd = random.randint(2, num - 2)
        testers.append(rnd)
        y = pow(rnd, num_dec, num)
        if(y != 1) & (y != num-1):
            j = 1
            while (j <= step-1) & (y != num-1):
                y = pow(y, 2, num)
                if y == 1:
                    print("Нарушено условие y == 1")
                    print("Число составное. Основание с нарушением:\t", rnd)
                    return
                j = j + 1
            if y != num-1:
                print("Нарушено условие y != num-1")
                print("Число составное. Основание с нарушением:\t", rnd)
                return
    print("Число, вероятно, простое. Был проверен массив оснований:")
    print(testers)
    return


def jacobi(a_j, n_j):
    a_j %= n_j
    result = 1
    while a_j != 0:
        while a_j % 2 == 0:
            a_j /= 2
            n_mod_8 = n_j % 8
            if n_mod_8 in (3, 5):
                result = -result
        a_j, n_j = n_j, a_j
        if a_j % 4 == 3 and n_j % 4 == 3:
            result = -result
        a_j %= n_j
    if n_j == 1:
        return result
    else:
        return 0


def sh(num, test_count):
    print("тест Соловэя Штрассена")
    testers = []
    num_dec = num-1
    step = 0
    it = 0
    for it in range(test_count):
        rnd = random.randint(2, num - 2)
        testers.append(rnd)
        res = pow(rnd, num_dec >> 1, num)
        # print("res ::\t", res)
        if(res != 1) & (res != num-1):
            print("Нарушено условие а != 1 и а != num-1")
            print("Число составное. Основание с нарушением:\t", rnd)
            return
        s = jacobi(rnd, num)
        if s < 0:
            while s < 0:
                s = s + num
        if s > num:
            s = s % num
        # print("s ::\t", s)
        if res != s:
            print("Нарушено условие а != символу Якоби")
            print("Число составное. Основание с нарушением:\t", rnd)
            return
    print("Число, вероятно, простое. Был проверен массив оснований:")
    print(testers)
    return


def pr_menu(count):
    i = 0
    opts = ["Все", "Тест Ферма", "Тест Соловэя-Штрассена", "Тест Рабина-Миллера"]
    while i < 4:
        if i == count:
            print("-->", opts[i])
        else:
            print(opts[i])
        i += 1
    return


def menu():
    max_v = 3
    min_v = 0
    count = min_v
    pr_menu(count)
    os.system("cls")
    pr_menu(count)
    while True:
        c = readchar.readkey()
        res = []
        for ch in c:
            res.append(ord(ch))
        if res[0] == 27:
            if res[2] == 65:
                os.system("cls")
                if count != min_v:
                    count -= 1
                else:
                    count = max_v
            elif res[2] == 66:
                os.system("cls")
                if count != max_v:
                    count += 1
                else:
                    count = min_v
            os.system("cls")
            pr_menu(count)
        elif res[0] == 13:
            break
    return count


if __name__ == "__main__":
    i = 0
    a = []
    N = int(input("Сколько чисел?"))
    for i in range(N):
        a.append(int(input()))
    b = menu()
    os.system("cls")
    for i in range(N):
        if (b == 0) | (b == 1):
            f(a[i], 5)
        if (b == 0) | (b == 2):
            sh(a[i], 5)
        if (b == 0) | (b == 3):
            rm(a[i], 5)
        print("\n----------------------------------------------------\n")
