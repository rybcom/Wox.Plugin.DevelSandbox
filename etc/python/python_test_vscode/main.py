
def sum(num):
    return (num - 1) * num / 2

def sum_loop(num):
    sum = 0
    for i in range(num):
        sum+=i
    return sum

def main():
    value = 6
    print('sum of num ',value, ' is ',sum(value))
    print('sum_loop of num ',value, ' is ',sum_loop(value))

if __name__ == "__main__":
    main()

