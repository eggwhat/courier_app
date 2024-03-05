const formatDeliveryStatus = (status: string) => {
  switch (status) {
    case 'inprogress':
      return "in progress";
    case 'InProgress':
      return "In progress";
    case 'cannotdeliver':
      return "cannot deliver";
    case 'CannotDeliver':
      return "Cannot deliver";
    default:
      return status; 
  }
};

export default formatDeliveryStatus;