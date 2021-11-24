import qs from 'qs';

export const isFalsy = (value: unknown) => (value === 0 ? false : !value);

type ObjectType = Record<string, any>;

/**
 * @Description: 去除对象中的空字符串
 * @param {ObjectType} object
 * @param {*} unknown
 * @return {*}
 */
export const cleanObject = (object: ObjectType): ObjectType => {
  const result = { ...object };
  Object.keys(result).forEach((key) => {
    const value = result[key];
    if (isFalsy(value)) {
      delete result[key];
    }
  });
  return result;
};

/**
 * @Description: 将对象转行为字符串
 * @param {object} data
 * @return {*}
 */
export const object2str = (data: ObjectType): string => {
  return qs.stringify(cleanObject(trimObject(data)));
};

/**
 * @Description: 去除对象中value中前后空格
 * @param {ObjectType} data
 * @return {*}
 */
export const trimObject = (data: ObjectType): ObjectType => {
  return Object.keys(data).reduce((cur: ObjectType, next: string) => {
    cur[next] = isNaN(data[next]) ? data[next].trim() : data[next];
    return cur;
  }, {});
};
