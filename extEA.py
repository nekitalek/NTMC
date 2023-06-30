from timeit import default_timer as timer
def extended_euclidean_algorithm(a, b): #Расширенный алгоритм Евклида
    if a < b:
        a, b = b, a
    Ichar, Rchar, Xchar, Ychar, Qchar = 'i', 'r', 'x', 'y', 'q'
    xi_1 = 1
    xi = 0
    yi_1 = 0
    yi = 1
    ri_1 = a
    ri = b
    i = 0
    temp_r, temp_x, temp_y = 1, 1, 1
    print("========================================================================\n\t Extended Euclidean algorithm")
    print("% 3c" % Ichar, "% 25c" % Rchar, "% 25c" % Xchar, "% 25c" % Ychar, "% 25c" % Qchar)
    print("% 3d" % i, "% 25d" % ri_1, "% 25d" % xi, "% 25d" % yi)
    while temp_r!=0:
        i+= 1
        q = int(ri_1/ri)
        temp_r = ri_1 % ri
        print("% 3d" %i,"% 25d" %ri_1, "% 25d" %xi, "% 25d" %yi, "% 25d" %q)
        if temp_r == 0:
            break
        temp_x = xi_1 - q*xi
        temp_y = yi_1 - q*yi
        xi_1=xi
        yi_1=yi
        xi=temp_x
        yi=temp_y
        ri_1 = ri
        ri = temp_r
    print("GCD = ", ri)
    print("First coefficient: ", xi)
    print("Второй коэфф: ", yi)
    return

def extended_binary_euclidean_algorithm(a, b):
    Ichar, Uchar, Vchar, Achar, Bchar, Cchar, Dchar  = 'i', 'u', 'v', 'A', 'B', 'C', 'D'
    g = 1
    while a % 2 == 0 and b % 2 == 0:
        a = a // 2
        b = b // 2
        g = g * 2
    u = a
    v = b
    A = 1
    B = 0
    C = 0
    D = 1
    i = 0
    print("========================================================================\n\t Extended binary Euclidean algorithm")
    print("% 3c" % Ichar, "% 42c" %Uchar, "% 42c" %Vchar, "%42c" % Achar, "% 42c" %Bchar, "% 42c" %Cchar, "% 42c" %Dchar)
    while u!=0:
        print("% 3d" %i,"% 42d" %u, "% 42d" %v, "% 42d" %A, "% 42d" %B, "% 42d" %C, "% 42d" %D)
        i += 1
        while u % 2 == 0:
            u = u // 2
            if A % 2 == 0 and B % 2 == 0:
                A = A // 2
                B = B // 2
            else:
                A = (A + b) // 2
                B = (B - a) // 2
        while v % 2 == 0:
            v = v // 2
            if C % 2 == 0 and D % 2 == 0:
                C = C // 2
                D = D // 2
            else:
                C = (C + b) // 2
                D = (D - a) // 2
        if u >= v:
            u = u - v
            A = A - C
            B = B - D
        else:
            v = v - u
            C = C - A
            D = D - B
    d = g * v
    x = C
    y = D
    print("GCD = ", int(d))
    print("First coefficient = ", int(x))
    print("Second coefficient = ", int(y))
    return

def extended_euclidean_algorithm_with_us(a, b):
    if a < b:
        a, b = b, a
    Ichar, Rchar, Xchar, Ychar, Qchar = 'i', 'r', 'x', 'y', 'q'
    xi_1 = 1
    xi = 0
    yi_1 = 0
    yi = 1
    ri_1 = a
    ri = b
    i = 0
    temp_r, temp_x, temp_y = 1, 1, 1
    print("========================================================================\n\t Extended binary Euclidean algorithm with truncated residuals")
    print("% 3c" % Ichar, "% 85c" % Rchar, "% 42c" % Xchar, "% 42c" % Ychar, "% 10c" % Qchar)
    print("% 3d" % i, "% 85d" % ri_1, "% 42d" % xi, "% 42d" % yi)
    while temp_r!=0:
        i+= 1
        q = int(ri_1/ri)
        temp_r = ri_1 % ri
        print("% 3d" %i,"% 85d" %ri_1, "% 42d" %xi, "% 42d" %yi, "% 10d" %q)
        if temp_r == 0:
            break
        temp_x = xi_1 - q*xi
        temp_y = yi_1 - q*yi
        xi_1=xi
        yi_1=yi
        xi=temp_x
        yi=temp_y
        if temp_r >= (ri/2):
            temp_r = ri - temp_r
            xi = xi_1 - xi
            yi = yi_1 - yi
        ri_1 = ri
        ri = temp_r
    print("GCD = ", ri)
    print("First coefficient: ", xi)
    print("Second coefficient: ", yi)
    return
# Enter your numbers here, examples given
A1 = 10280584362558385364313785041802045979548186541286500517257326693113600668924649
B1 = 14598422008191898215363941644699891185829894490005557691091371425993340283543011
A2 = 10280584362558385364313785041802045979548186541286500517257326693113600668924649
B2 = 14598422008191898215363941644699891185829894490005557691091371425993340283543011
A3 = 10280584362558385364313785041802045979548186541286500517257326693113600668924649
B3 = 14598422008191898215363941644699891185829894490005557691091371425993340283543011

start = timer()
extended_euclidean_algorithm(A1, B1)
print("Time: {:g} secs".format(timer() - start))
start = timer()
extended_binary_euclidean_algorithm(A2, B2)
print("Time: {:g} secs".format(timer() - start))
start = timer()
extended_euclidean_algorithm_with_us(A3, B3)
print("Time: {:g} secs".format(timer() - start))
