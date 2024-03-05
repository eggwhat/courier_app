const formatOfferStatus = (status: string) => {
    switch (status) {
      case 'waitingfordecision':
        return "waiting for decision";
      case 'WaitingForDecision':
        return "Waiting for decision";
      case 'pickedup':
        return "picked up";
      case 'PickedUp':
        return "Picked up";
      case 'cannotdeliver':
        return "cannot deliver";
      case 'CannotDeliver':
        return "Cannot deliver";
      default:
        return status; 
    }
};

export default formatOfferStatus;