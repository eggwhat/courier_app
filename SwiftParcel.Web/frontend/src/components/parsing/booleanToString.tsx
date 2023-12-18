const booleanToString = (value : any) => {
    if (typeof value === 'boolean') {
      if (value == true) {
        return 'true'
      } else if (value == false) {
        return 'false';
      }
    }
    return 'false';
};

export default booleanToString;
